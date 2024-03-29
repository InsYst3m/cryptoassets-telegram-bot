﻿using NotificationBot.Telegram.Infrastructure.GraphService;
using NotificationBot.Telegram.Infrastructure.Services.Interfaces;
using NotificationBot.Telegram.Infrastructure.ViewModels;

using StrawberryShake;

namespace NotificationBot.Telegram.Infrastructure.Services
{
	public class GraphService : IGraphService
	{
		private readonly ICryptoAssetsGraphServiceClient _graphService;

		public GraphService(ICryptoAssetsGraphServiceClient graphService)
		{
			ArgumentNullException.ThrowIfNull(graphService);

			_graphService = graphService;
		}

		public async Task<string[]> GetSupportedCryptoAssetsAsync()
		{
			IOperationResult<IGetSupportedCryptoAssetsResult> result = await _graphService.GetSupportedCryptoAssets.ExecuteAsync();

			result.EnsureNoErrors();

			if (result.Data is not null)
			{
				return result.Data.SupportedCryptoAssets.ToArray();
			}

			return Array.Empty<string>();
		}

		/// <inheritdoc cref="IGraphService.GetCryptoAssetAsync(string, CancellationToken)"/>
		public async Task<CryptoAssetViewModel?> GetCryptoAssetAsync(string abbreviation, CancellationToken cancellationToken)
		{
			IOperationResult<IGetCryptoAssetResult> cryptoAssetGraphData =
				await _graphService.GetCryptoAsset.ExecuteAsync(abbreviation, cancellationToken);

			cryptoAssetGraphData.EnsureNoErrors();

			return MapToCryptoAssetViewModel(cryptoAssetGraphData.Data?.CryptoAsset);
		}

		/// <inheritdoc cref="IGraphService.GetCryptoAssetsAsync(string[], CancellationToken)"/>
		public async Task<List<CryptoAssetViewModel>> GetCryptoAssetsAsync(string[] abbreviations, CancellationToken cancellationToken)
		{
			List<CryptoAssetViewModel> result = new();

			foreach (string abbreviation in abbreviations)
			{
				IOperationResult<IGetCryptoAssetResult> cryptoAssetGraphData =
				await _graphService.GetCryptoAsset.ExecuteAsync(abbreviation, cancellationToken);

				cryptoAssetGraphData.EnsureNoErrors();

				CryptoAssetViewModel? cryptoAsset = MapToCryptoAssetViewModel(cryptoAssetGraphData.Data?.CryptoAsset);

				if (cryptoAsset is not null)
				{
					result.Add(cryptoAsset);
				}
			}

			return result;
		}

		#region Internal Implementation

		private CryptoAssetViewModel? MapToCryptoAssetViewModel(IGetCryptoAsset_CryptoAsset? cryptoAssetGraphData)
		{
			if (cryptoAssetGraphData == null)
			{
				return null;
			}

			return new CryptoAssetViewModel(
				cryptoAssetGraphData.Name,
				cryptoAssetGraphData.Abbreviation,
				cryptoAssetGraphData.Rank,
				cryptoAssetGraphData.CapitalizationUsd,
				cryptoAssetGraphData.CurrentPriceUsd,
				cryptoAssetGraphData.AllTimeHighPriceUsd,
				cryptoAssetGraphData.AllTimeLowPriceUsd,
				cryptoAssetGraphData.HighTwentyFourHoursUsd,
				cryptoAssetGraphData.LowTwentyFourHoursUsd,
				cryptoAssetGraphData.AllTimeHighChangePercentage,
				cryptoAssetGraphData.AllTimeLowChangePercentage);
		}

		#endregion

	}
}
