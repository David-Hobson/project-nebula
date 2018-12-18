using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

public class CutsceneTest {

    [Test]
    public void CheckCutsceneStarted() {
        // Use the Assert class to test conditions.
        var cutscene = new GameObject().AddComponent<Cutscene>();
        Assert.AreEqual(1, cutscene.CutsceneStart());
    }

    [Test]
    public void CheckCutsceneCompleted()
    {
        // Use the Assert class to test conditions.
        var cutscene = new GameObject().AddComponent<Cutscene>();
        Assert.AreEqual(2, cutscene.CutsceneCompleted());
    }

    [Test]
    public void CheckCutscenePauseSelection()
    {
        // Use the Assert class to test conditions.
        var cutscene = new GameObject().AddComponent<Cutscene>();
        Assert.AreEqual(3, cutscene.CutscenePauseSelection());
    }

    [Test]
    public void CheckCutsceneUnpauseSelection()
    {
        // Use the Assert class to test conditions.
        var cutscene = new GameObject().AddComponent<Cutscene>();
        Assert.AreEqual(4, cutscene.CutsceneUnpauseSelection());
    }

    [Test]
    public void CheckCutsceneSkipSelection()
    {
        // Use the Assert class to test conditions.
        var cutscene = new GameObject().AddComponent<Cutscene>();
        Assert.AreEqual(5, cutscene.CutsceneSkipSelection());
    }

    [Test]
    public void CheckCutsceneResumeSelection()
    {
        // Use the Assert class to test conditions.
        var cutscene = new GameObject().AddComponent<Cutscene>();
        Assert.AreEqual(6, cutscene.CutsceneResumeSelection());
    }


}
