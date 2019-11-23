using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    public AudioMixer audioMixer;
    public Dropdown resolutionDropdown;
    private Resolution[] resolutions;

    private void Start()
    {
        if(resolutionDropdown != null)
            SetupResolutions();
    }
    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("volume", volume);
        Debug.Log(volume);
    }

    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }

    /**Input: int
     * Output: none
     * SetResolution updates the current resolution to the given index resolutionIndex
     **/
    public void SetResolution(int resolutionIndex)
    {
        Resolution chosenRes = resolutions[resolutionIndex];
        Screen.SetResolution(chosenRes.width, chosenRes.height, Screen.fullScreen);
    }

    /**Input: None
     * OutputL None
     * SetupResolutions sets up the resolutions that are available from the client and
     * puts them into the dropdown list. 
     * resolutionDrop variable needs a reference to the Dropdown GameObject
     * 
     **/
    private void SetupResolutions()
    {
        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();
        List<string> options = new List<string>();

        int defaultResolutionIndex = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            string optionStr = resolutions[i].width + "x" + resolutions[i].height;
            options.Add(optionStr);
            if (ResolutionsAreEqual(resolutions[i], Screen.currentResolution))
                defaultResolutionIndex = i;
        }
        resolutionDropdown.AddOptions(options);
        // Sets the current resolution of the client as the default in game, 
        // refreshshownvalue shows this choice into the UI
        resolutionDropdown.value = defaultResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }

    /**Input: Resolution - Two Resolutions
     * output: bool
     * ResolutionsAreEqual compares the pixel width and pixel height of both resolutions
     * to check that they are equal, unity does not have an equals comparator integrated 
     * for resolution checking.
     * **/
    private bool ResolutionsAreEqual(Resolution resA, Resolution resB)
    {
        if (resA.width == resB.width &&
           resA.height == resB.height)
            return true;
        return false;
    }
}
