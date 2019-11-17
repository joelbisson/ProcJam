using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Layout {
    public int spacing = 16;
    public int imageGridSize = 16;
}

[Serializable]
public class SpriteConfig {
    public int spritePixelSize = 16;
}
[Serializable]
public class NoiseConfig{
    public bool randomizeFrequency;
    public List<Octave> octaves = new List<Octave> {
        new Octave(5f, .8f),
        new Octave(10f, .25f),
        new Octave(20f, .125f),
        new Octave(45, .0625f)
    };
    public bool randomOrigin = true;
    public float randomOriginBound = 255f;
    public Vector2 manualOrigin;
    public float animationFrameNoiseOffset = .2f;
}[Serializable]
public class FalloffConfig{
    public AnimationCurve falloffCurve;
}
[Serializable]
public class SpriteColorConfig{
    public bool colorEnabled;
    public int paletteIndex;
    public int colorCountPerSprite;
    public bool overridePaletteColorsWithRandomColors;
}
[Serializable]
public class BackgroundColorConfig{
    public bool randomPaletteColorForBackground;
    public int paletteColorIndexForBackground;
    public bool overrideBackgroundColor;
    public Color backgroundColorOverride;
}
[Serializable]
public class OutlineConfig{
    public bool outlineEnabled;
    public bool applyOutlineAfterScaling;

    public bool randomPaletteColorForOutline;
    public int paletteColorIndexForOutline;
    public bool overrideOutlineColor;
    public Color outlineColorOverride;
}[Serializable]
public class SymmetryConfig{
    [Range(0f,1f)] public float horizontalSymmetryChance;
    [Range(0f,1f)] public float verticalSymmetryChance;
    [Range(0f,1f)] public float backwardDiagonalSymmetryChance;
    [Range(0f,1f)] public float forwardDiagonalSymmetryChance;
    public bool allowQuarterSymmetry;
}
[Serializable]
public class ScalingConfig{
    public ScalingMode[] scalingModes;
    public FilterMode filterMode;
}
[Serializable]
public class AnimationConfig{
[Range(1, 8)] public int animationFrameCount = 1;
public float timeBetweenFrames;
public AnimationMode animationMode;
}
[Serializable]
public class CleanupConfig{
    public bool allowPixelsOnEdgeOfSprite;
    public LonePixelEvaluationMode lonePixelEvaluationMode;
    [Range(0f,1f)] public float chanceToDeleteLonePixels;
}
[Serializable]
public class Configuration {
    public Layout layout;
    public SpriteConfig spriteConfig;
    public NoiseConfig noiseConfig;
    public FalloffConfig falloffConfig;
    public SpriteColorConfig spriteColorConfig;
    public BackgroundColorConfig backgroundColorConfig;
    public OutlineConfig outlineConfig;
    public SymmetryConfig symmetryConfig;
    public ScalingConfig scalingConfig;
    public AnimationConfig animationConfig;
    public CleanupConfig cleanupConfig;
}

[Serializable]
public class Preset {
    public string name;
    public Configuration configuration;

    public Preset(string name, Configuration config) {
        this.name = name;
        configuration = config;
    }
}

public enum Presets {
    none = 0, bitsy, island, spaceships, advanced_spaceships, transforming_spaceships,
    pocket_monsters, pocket_monsters_low_rez_symmetrical, pocket_monsters_low_rez_outlined_imperfect_symmetry
}

public enum ScalingMode { none, x2, x4, x10, eagle2, eagle3 }
public enum AnimationMode { loop, pingPong }
public enum LonePixelEvaluationMode { CardinalDirectionsOnly, IncludeDiagonals }
