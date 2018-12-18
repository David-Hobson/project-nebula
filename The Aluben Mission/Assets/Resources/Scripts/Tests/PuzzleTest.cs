using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

public class PuzzleTest {

    [Test]
    public void CheckPuzzleStart()
    {
        // Use the Assert class to test conditions.
        var puzzle = new GameObject().AddComponent<Puzzle>();
        Assert.AreEqual(1, puzzle.PuzzleStart());
    }

    [Test]
    public void CheckPuzzleInProgress()
    {
        // Use the Assert class to test conditions.
        var puzzle = new GameObject().AddComponent<Puzzle>();
        Assert.AreEqual(2, puzzle.PuzzleInProgress());
    }

    [Test]
    public void CheckPuzzleCompleted()
    {
        // Use the Assert class to test conditions.
        var puzzle = new GameObject().AddComponent<Puzzle>();
        Assert.AreEqual(3, puzzle.PuzzleCompleted());
    }

    [Test]
    public void CheckPuzzleInProgressState()
    {
        // Use the Assert class to test conditions.
        var puzzle = new GameObject().AddComponent<Puzzle>();
        Assert.AreEqual(4, puzzle.PuzzleInProgressState());
    }

    [Test]
    public void CheckPuzzleFailed()
    {
        // Use the Assert class to test conditions.
        var puzzle = new GameObject().AddComponent<Puzzle>();
        Assert.AreEqual(5, puzzle.PuzzleFailed());
    }

    [Test]
    public void CheckPuzzleRewards()
    {
        // Use the Assert class to test conditions.
        var puzzle = new GameObject().AddComponent<Puzzle>();
        Assert.AreEqual(6, puzzle.PuzzleRewards());
    }
}
