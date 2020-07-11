using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(AudioFilter))]
public class AudioFilterEditor : Editor
{
    public override void OnInspectorGUI()
    {
        //base.OnInspectorGUI();

        AudioFilter filterTarget = (AudioFilter)target;

        filterTarget.chorus = EditorGUILayout.Toggle("Chorus", filterTarget.chorus);

        if (filterTarget.chorus)
        {
            EditorGUI.indentLevel = 1;
            filterTarget.chorusFilter.dryMix = EditorGUILayout.FloatField("Dry Mix", filterTarget.chorusFilter.dryMix);
            filterTarget.chorusFilter.wetMix1 = EditorGUILayout.FloatField("Wet Mix 1", filterTarget.chorusFilter.wetMix1);
            filterTarget.chorusFilter.wetMix2 = EditorGUILayout.FloatField("Wet Mix 2", filterTarget.chorusFilter.wetMix2);
            filterTarget.chorusFilter.wetMix3 = EditorGUILayout.FloatField("Wet Mix 3", filterTarget.chorusFilter.wetMix3);
            filterTarget.chorusFilter.delay = EditorGUILayout.FloatField("Delay", filterTarget.chorusFilter.delay);
            filterTarget.chorusFilter.rate = EditorGUILayout.FloatField("Rate", filterTarget.chorusFilter.rate);
            filterTarget.chorusFilter.depth = EditorGUILayout.FloatField("Depth", filterTarget.chorusFilter.depth);
        }

        EditorGUI.indentLevel = 0;
        filterTarget.distortion = EditorGUILayout.Toggle("Distortion", filterTarget.distortion);

        if (filterTarget.distortion)
        {
            EditorGUI.indentLevel = 1;
            filterTarget.distortionLevel = EditorGUILayout.FloatField("Distortion Level", filterTarget.distortionLevel);
        }

        EditorGUI.indentLevel = 0;
        filterTarget.echo = EditorGUILayout.Toggle("Echo", filterTarget.echo);

        if (filterTarget.echo)
        {
            EditorGUI.indentLevel = 1;
            filterTarget.echoFilter.delay = EditorGUILayout.FloatField("Delay", filterTarget.echoFilter.delay);
            filterTarget.echoFilter.decayRatio = EditorGUILayout.FloatField("Decay Ratio", filterTarget.echoFilter.decayRatio);
            filterTarget.echoFilter.wetMix = EditorGUILayout.FloatField("Wet Mix", filterTarget.echoFilter.wetMix);
            filterTarget.echoFilter.dryMix = EditorGUILayout.FloatField("Dry Mix", filterTarget.echoFilter.dryMix);
        }

        EditorGUI.indentLevel = 0;
        filterTarget.highPass = EditorGUILayout.Toggle("High Pass", filterTarget.highPass);

        if (filterTarget.highPass)
        {
            EditorGUI.indentLevel = 1;
            filterTarget.highPassFilter.cutOffFrequency = EditorGUILayout.Slider("Cut Off Frequency", filterTarget.highPassFilter.cutOffFrequency, 10f, 22000f);
            filterTarget.highPassFilter.highpassResonanceQ = EditorGUILayout.FloatField("Highpass Resonance Q", filterTarget.highPassFilter.highpassResonanceQ);
        }

        EditorGUI.indentLevel = 0;
        filterTarget.lowPass = EditorGUILayout.Toggle("Low Pass", filterTarget.lowPass);

        if (filterTarget.lowPass)
        {
            EditorGUI.indentLevel = 1;
            filterTarget.lowPassFilter.cutOffFrequency = EditorGUILayout.Slider("Cut Off Frequency", filterTarget.lowPassFilter.cutOffFrequency, 10f, 22000f);
            filterTarget.lowPassFilter.lowpassResonanceQ = EditorGUILayout.FloatField("Highpass Resonance Q", filterTarget.lowPassFilter.lowpassResonanceQ);
        }

        EditorGUI.indentLevel = 0;
        filterTarget.reverb = EditorGUILayout.Toggle("Reverb", filterTarget.reverb);

        if (filterTarget.reverb)
        {
            EditorGUI.indentLevel = 1;
            filterTarget.reverbFilter.dryLevel = EditorGUILayout.Slider("Dry Level", filterTarget.reverbFilter.dryLevel, -10000f, 0f);
            filterTarget.reverbFilter.room = EditorGUILayout.Slider("Room", filterTarget.reverbFilter.room, -10000f, 0f);
            filterTarget.reverbFilter.roomHF = EditorGUILayout.Slider("Room HF", filterTarget.reverbFilter.roomHF, -10000f, 0f);
            filterTarget.reverbFilter.roomLF = EditorGUILayout.Slider("Room LF", filterTarget.reverbFilter.roomLF, -10000f, 0f);
            filterTarget.reverbFilter.decayTime = EditorGUILayout.Slider("Decay Time", filterTarget.reverbFilter.decayTime, 0.1f, 20f);
            filterTarget.reverbFilter.decayHFRatio = EditorGUILayout.Slider("Decay HF Ratio", filterTarget.reverbFilter.decayHFRatio, 0.1f, 2f);
            filterTarget.reverbFilter.reflectionsLevel = EditorGUILayout.Slider("Reflection Level", filterTarget.reverbFilter.reflectionsLevel, -10000f, 1000f);
            filterTarget.reverbFilter.reflectionsDelay = EditorGUILayout.Slider("Reflection Delay", filterTarget.reverbFilter.reflectionsDelay, 0f, 0.3f);
            filterTarget.reverbFilter.reverbLevel = EditorGUILayout.Slider("Reverb Level", filterTarget.reverbFilter.reverbLevel, -10000f, 2000f);
            filterTarget.reverbFilter.reverbDelay = EditorGUILayout.Slider("Reverb Delay", filterTarget.reverbFilter.reverbDelay, 0f, 0.1f);
            filterTarget.reverbFilter.hfReference = EditorGUILayout.Slider("HF Reference", filterTarget.reverbFilter.hfReference, 1000f, 20000f);
            filterTarget.reverbFilter.lfReference = EditorGUILayout.Slider("LF Reference", filterTarget.reverbFilter.lfReference, 20f, 1000f);
            filterTarget.reverbFilter.diffusion = EditorGUILayout.Slider("Diffusion", filterTarget.reverbFilter.diffusion, 0f, 100f);
            filterTarget.reverbFilter.density = EditorGUILayout.Slider("Density", filterTarget.reverbFilter.density, 0f, 100f);
        }
    }
}
