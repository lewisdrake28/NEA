//
//  APITypes.swift
//  Music
//
//  Created by Lewis Drake on 03/11/2022.
//
// 16:08

import UIKit

extension API {
    enum Types {
        enum Response {
            struct ArtistSearch: Decodable {
                var resultCount: Int
                var results: [Result]

                struct Result: Decodable {
                    var artistId: Int
                    var trackName: String
                    var artworkUrl100: String
                    var trackId: Int
                }
            }
            
            struct ArtistLookup: Decodable {
                var resultCount: Int
                var results: [Result]
                
                struct Result: Decodable {
                    var artistName: String
                    var artistLinkUrl: String
                    var primaryGenreName: String
                }
            }
        }

        enum Request {
            struct Empty: Encodable { }
        }

        enum Error: LocalizedError {
            case generic(reason: String)
            case `internal`(reason: String)

            var errorDescription: String? {
                switch self {
                case .generic(let reason):
                    return reason
                case .internal(let reason):
                    return "Internal error: \(reason)"
                }
            }
        }

        enum Endpoint {
            case search(query: String)
            case lookup(id: Int)

            var url: URL {
                var components = URLComponents()
                components.host = "itunes.apple.com"
                components.scheme = "https"

                switch self {
                case .search(let query):
                    components.path = "/search"
                    components.queryItems = [
                        URLQueryItem(name: "term", value: query),
                        URLQueryItem(name: "media", value: "music"),
                        URLQueryItem(name: "attribute", value: "artistTerm")
                    ]
                case .lookup(let id):
                    components.path = "/lookup"
                    components.queryItems = [
                        URLQueryItem(name: "id", value: "\(id)"),
                    ]
                }
                return components.url!
            }
        }
        
        enum Method: String {
            case get
            case post
        }
    }
}
