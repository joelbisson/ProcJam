﻿using System.Collections.Generic;
using UnityEngine;

public class Recoloring : MonoBehaviour {

    [Header("Palettes")]
    public Texture2D[] palettes;

    [Header("Debug")]
    [SerializeField] Color[] generatedColors;
    [SerializeField] public List<Color>[] uniqueColorsInTextures;

    Camera cam;

    void OnEnable() {
        uniqueColorsInTextures = new List<Color>[palettes.Length];
        for (var index = 0; index < palettes.Length; index++) {
            var texture2D = palettes[index];
            uniqueColorsInTextures[index] = GetUniqueColorsFromTexture(texture2D);
        }
    }
    
    public (Color, Color) Recolor(ref Texture2D tex, int frame, ColorConfig colorConfig, BackgroundColorConfig backgroundColorConfig, OutlineConfig outlineConfig) {
        if (!cam) cam = Camera.main;
        cam.backgroundColor = BackgroundColor(backgroundColorConfig, colorConfig);
        if (frame == 0)
            GenerateColors(colorConfig, backgroundColorConfig);
        var colors = tex.GetPixels();
        var increment = 1f / colorConfig.colorCountPerSprite;
        var newColors = new Color[colors.Length];
        for (var index = 0; index < colors.Length; index++) {
            var gray = colors[index].grayscale;
            for (var i = 0; i < colorConfig.colorCountPerSprite; i++) {
                if (gray >= i * increment && gray <= (i + 1) * increment)
                    newColors[index] = generatedColors[i];
            }
        }
        tex.SetPixels(newColors);
        return (BackgroundColor(backgroundColorConfig, colorConfig), OutlineColor(outlineConfig));
    }

    Color OutlineColor(OutlineConfig outlineConfig) {
        if (outlineConfig.overrideOutlineColor)
            return outlineConfig.outlineColorOverride;
        if (outlineConfig.randomPaletteColorForOutline)
            return generatedColors[Random.Range(0, generatedColors.Length - 1)];
        return generatedColors[outlineConfig.paletteColorIndexForOutline];
    }

    Color BackgroundColor(BackgroundColorConfig backgroundColorConfig, ColorConfig colorConfig) {
        if (!colorConfig.usePaletteColors)
            return Color.black;
        if (backgroundColorConfig.overrideBackgroundColor)
            return backgroundColorConfig.backgroundColorOverride;
        if (backgroundColorConfig.randomPaletteColorForBackground)
            return generatedColors[Random.Range(0, generatedColors.Length - 1)];
        return generatedColors[backgroundColorConfig.paletteColorIndexForBackground];
    }

    void GenerateColors(ColorConfig colorConfig, BackgroundColorConfig backgroundColorConfig) {
        generatedColors = new Color[colorConfig.colorCountPerSprite];
        if (colorConfig.usePaletteColors) {
            if (backgroundColorConfig.overrideBackgroundColor)
                generatedColors[0] = backgroundColorConfig.backgroundColorOverride;
            else if (backgroundColorConfig.randomPaletteColorForBackground) {
                generatedColors[0] = uniqueColorsInTextures[colorConfig.paletteIndex][
                    Random.Range(0, uniqueColorsInTextures[colorConfig.paletteIndex].Count - 1)];
            } else {
                generatedColors[0] =
                    uniqueColorsInTextures[colorConfig.paletteIndex][
                        backgroundColorConfig.paletteColorIndexForBackground];
            }
            
            for (var index = 1; index < generatedColors.Length; index++) {
                generatedColors[index] = uniqueColorsInTextures[colorConfig.paletteIndex][Random.Range(0, uniqueColorsInTextures[colorConfig.paletteIndex].Count)];
                //uniqueColorsInTextures.Remove(generatedColors[index]);
            }
        }
        else {
            generatedColors[0] = Color.black;
            for (var index = 1; index < generatedColors.Length; index++) 
                generatedColors[index] = Random.ColorHSV(0f, 1f, 1f, 1f);
        }
    }

    List<Color> GetUniqueColorsFromTexture(Texture2D texture) {
        var uniqueColors = new List<Color>();
        foreach (var col in texture.GetPixels()) {
            if (!uniqueColors.Contains(col))
                uniqueColors.Add(col);
        }
        return uniqueColors;
    }
}
