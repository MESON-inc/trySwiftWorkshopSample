import Foundation
import SwiftUI
import UnityFramework

struct SampleInjectedScene {
    
    @SceneBuilder
    static var scene: some Scene {
        WindowGroup(id: "SampleScene") {
            SampleContentView(scoreData: getScoreData() ?? ScoreData())
        }.defaultSize(width: 400.0, height: 400.0)
    }
}
