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

typealias SampleCallbackDelegateType = @convention(c) (UnsafePointer<CChar>) -> Void

var sSampleCallbackDelegate: SampleCallbackDelegateType? = nil

@_cdecl("SetSampleNativeCallback")
func setSampleNativeCallback(_ delegate: SampleCallbackDelegateType) {
    print("----> Set native callback")
    sSampleCallbackDelegate = delegate
}

@_cdecl("CloseSwiftUiSampleWindow")
func closeSwiftUISampleWindow(_ cname: UnsafePointer<CChar>) {
    let dismissWindow = EnvironmentValues().dismissWindow
    let name = String(cString: cname)
    dismissWindow(id: name)
}

// ----------------------------------------------------

@_cdecl("OpenSwiftUiSampleWindow")
func openSwiftUISampleWindow(_ cname: UnsafePointer<CChar>) {
    let openWindow = EnvironmentValues().openWindow
    let name = String(cString: cname)
    openWindow(id: name)
}

@_cdecl("UpdateScore")
func updateScore(_ score: Int32) {
    scoreDataProvider.scoreData.score = score
}

public func callCSharpSampleCallback(_ str: String) {
    guard let callback = sSampleCallbackDelegate else {
        return
    }
    
    str.withCString {
        callback($0)
    }
}
