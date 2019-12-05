using System;
using UnityEngine;

[Serializable]
public class GeneratedTexture {
    public Texture2D texture;
    public Texture2D normal;
    public Vector2 origin;
    public SymmetryOutcome symmetryOutcome;
}

[Serializable]
public class SymmetryOutcome {
    public bool horizontalSymmetryResult;
    public bool verticalSymmetryResult;
    public bool forwardDiagonalSymmetryResult;
    public bool backwardDiagonalSymmetryResult;
    public bool quarterHorizontalSymmetryResult;
    public bool quarterVerticalSymmetryResult;
    public bool quarterForwardDiagonalSymmetryResult;
    public bool quarterBackwardDiagonalSymmetryResult;
    public bool lowerIsDominant;
    public SymmetryOutcome(
        bool horizontalSymmetryResult, 
        bool verticalSymmetryResult,
        bool forwardDiagonalSymmetryResult,
        bool backwardDiagonalSymmetryResult,
        bool quarterHorizontalSymmetryResult,
        bool quarterVerticalSymmetryResult,
        bool quarterForwardDiagonalSymmetryResult,
        bool quarterBackwardDiagonalSymmetryResult,
        bool lowerIsDominant) {
        this.horizontalSymmetryResult = horizontalSymmetryResult; 
        this.verticalSymmetryResult = verticalSymmetryResult; 
        this.forwardDiagonalSymmetryResult = forwardDiagonalSymmetryResult; 
        this.backwardDiagonalSymmetryResult = backwardDiagonalSymmetryResult; 
        this.quarterHorizontalSymmetryResult = quarterHorizontalSymmetryResult; 
        this.quarterVerticalSymmetryResult = quarterVerticalSymmetryResult; 
        this.quarterForwardDiagonalSymmetryResult = quarterForwardDiagonalSymmetryResult; 
        this.quarterBackwardDiagonalSymmetryResult = quarterBackwardDiagonalSymmetryResult;
        this.lowerIsDominant = lowerIsDominant;
    }
    public SymmetryOutcome(){}
}

public class InheritedSymmetryConfig {
    public bool inherited;
    public SymmetryOutcome outcome;

    public InheritedSymmetryConfig(bool inherited, SymmetryOutcome outcome = null) {
        this.inherited = inherited;
        this.outcome = outcome;
    }
}