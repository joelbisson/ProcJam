﻿﻿using System;
 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteGeneration : MonoBehaviour
{
    [SerializeField] int imageSize = 32;

    [Header("Filtering")]
    [SerializeField] Recoloring recoloring;
    [SerializeField] NoiseGeneration noiseGeneration;
    [SerializeField] Symmetry symmetry;
    [SerializeField] Falloff falloff;
    [SerializeField] Outline outline;
    [SerializeField] Scaling scaling;
    
    public List<Sprite> Generate(Configuration configuration) {
        var sprites = new List<Sprite>();
        for (int i = 0; i < configuration.animationFrameCount; i++) {
            sprites.Add(Sprite.Create(GenerateTexture(i, configuration), RectAccordingToScalingMode(configuration.scalingMode), new Vector2(.5f, .5f)));
        }
        return sprites;
    }

    Texture2D GenerateTexture(int frame, Configuration configuration)
    {
        var tex = noiseGeneration.GetNoise(imageSize, frame);
        falloff.ApplyFalloff(ref tex);
        symmetry.AttemptToApplySymmetry(ref tex, frame);
        var (backgroundColor, outlineColor) = recoloring.Recolor(ref tex, frame);
        outline.OutlineTexture(ref tex, backgroundColor, outlineColor);
        scaling.ScaleTexture(ref tex, configuration.scalingMode);
        tex.filterMode = FilterMode.Point;
        tex.Apply();    
        return tex;
    }

    Rect RectAccordingToScalingMode(ScalingMode configurationScalingMode) {
        switch (configurationScalingMode) {
            case ScalingMode.none:
                return new Rect(0, 0, imageSize, imageSize);
            case ScalingMode.x2:
                return new Rect(0, 0, imageSize * 2, imageSize * 2);
            case ScalingMode.x4:
                return new Rect(0, 0, imageSize * 4, imageSize * 4);
                break;
            case ScalingMode.eagle2:
                break;
            case ScalingMode.eagle3:
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(configurationScalingMode), configurationScalingMode, null);
        }
        return new Rect(0, 0, imageSize, imageSize);
    }

    Texture2D EmptyTexture() => new Texture2D(imageSize, imageSize);
}
