import Foundation
import SwiftUI

public class ScoreData: ObservableObject {
    @Published public var score: Int32 = 0
    
    public init() {
    }
}

var scoreData: ScoreData?

func tryCreateScoreData() {
    if scoreData == nil {
        scoreData = ScoreData()
    }
}

public func getScoreData() -> ScoreData? {
    tryCreateScoreData()
    
    guard let data = scoreData else {
        return nil
    }
        
    return data
}

// ---------------------------------------------------

typealias SampleCallbackDelegateType = @convention(c) (UnsafePointer<CChar>) -> Void

var sSampleCallbackDelegate: SampleCallbackDelegateType? = nil

public func callCSharpSampleCallback(_ str: String)
{
    guard let callback = sSampleCallbackDelegate else {
        return
    }

    str.withCString {
        callback($0)
    }
}

@_cdecl("UpdateScore")
func updateScore(_ score: Int32) {
    guard let scoreData = getScoreData() else {
        return
    }
    
    scoreData.score = score
}

@_cdecl("SetSampleNativeCallback")
func setSampleNativeInputCallback(_ delegate: SampleCallbackDelegateType)
{
    print("############ SET NATIVE CALLBACK")
    sSampleCallbackDelegate = delegate
}

@_cdecl("OpenSwiftUiSampleWindow")
func openSwiftUIInputWindow(_ cname: UnsafePointer<CChar>)
{
    let openWindow = EnvironmentValues().openWindow

    let name = String(cString: cname)
    print("############ OPEN WINDOW \(name)")
    openWindow(id: name)
}

@_cdecl("CloseSwiftUiSampleWindow")
func closeSwiftUIInputWindow(_ cname: UnsafePointer<CChar>)
{
    let dismissWindow = EnvironmentValues().dismissWindow

    let name = String(cString: cname)
    print("############ CLOSE WINDOW \(name)")
    dismissWindow(id: name)
}
