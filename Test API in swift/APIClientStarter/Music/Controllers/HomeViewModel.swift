//
//  ViewModel.swift
//  Images
//
//  Created by Cristi Habliuc on 16.03.2022.
//

import UIKit

extension HomeViewController {

	class ViewModel {
		private var results: [Release] = []

		var coverSize: CGSize = CGSize(width: 100, height: 100)

		var onResultsReceived: (() -> Void)?

		var onError: ((String) -> Void)?

		func fetchResults(withQuery query: String) {
			API.Client.shared
				.get(.search(query: query)) { (result: Result<API.Types.Response.ArtistSearch, API.Types.Error>) in
				DispatchQueue.main.async {
					switch result {
					case .success(let success):
						self.parseResults(success)
					case .failure(let failure):
						self.onError?(failure.localizedDescription)
					}
				}
			}
		}

		func handleReleaseSelection(at indexPath: IndexPath) {
			guard indexPath.row >= 0 && indexPath.row < results.count else {
				return
			}

			let release = results[indexPath.row]
			API.Client.shared
				.get(.lookup(id: release.artistId)) { (result: Result<API.Types.Response.ArtistLookup, API.Types.Error>) in
				DispatchQueue.main.async {
					switch result {
					case .success(let success):
						print("THe artist details came in: \(success)")
					case .failure(let failure):
						print("Error while getting artist info: \(failure.localizedDescription)")
					}
				}
			}
		}

		private func parseResults(_ results: API.Types.Response.ArtistSearch) {
			var localResults = [Release]()

			for result in results.results {
				let localResult = Release(
					id: result.trackId,
					imageUrl: convertCoverUrl(result.artworkUrl100),
					title: result.trackName,
					artistId: result.artistId)
				localResults.append(localResult)
			}

			self.results = localResults
			onResultsReceived?()
		}

		private func convertCoverUrl(_ from: String) -> URL {
			return URL(string: from.replacingOccurrences(of: "100x100", with: "\(Int(coverSize.width))x\(Int(coverSize.height))"))!
		}

		func coverURL(for indexPath: IndexPath) -> URL? {
			guard indexPath.row >= 0 && indexPath.row < results.count else {
				return nil
			}
			return results[indexPath.row].imageUrl
		}

		func numberOfResults() -> Int {
			return results.count
		}
	}

}

extension HomeViewController.ViewModel {
	struct Release {
		var id: Int
		var imageUrl: URL
		var title: String
		var artistId: Int
	}
}
