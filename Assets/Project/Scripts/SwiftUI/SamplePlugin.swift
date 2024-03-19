import Foundation
import SwiftUI

public class ScoreData: ObservableObject {
    @Published public var score: Int32 = 0
    
    public init() {
    }
}

public class ScoreDataProvider {
    private let _scoreData: ScoreData = ScoreData()

    public var scoreData: ScoreData {
        _scoreData
    }
}

public let scoreDataProvider: ScoreDataProvider = ScoreDataProvider()

// ---------------------------------------------------

