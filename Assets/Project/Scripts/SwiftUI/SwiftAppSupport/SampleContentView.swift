import Foundation
import SwiftUI
import UnityFramework
import RealityKit

struct SampleContentView: View {
    @State var message: String = ""
    @FocusState var isFocused: Bool
    @ObservedObject var scoreData: ScoreData
    
    var body: some View {
        VStack {
            Text("Your score is \(scoreData.score)")
            
            Button("Restart") {
                callCSharpSampleCallback("Restart")
            }
        }
    }
}
