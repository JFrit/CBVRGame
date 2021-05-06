﻿using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UXF;

namespace ScriptableObjects
{
    [CreateAssetMenu(menuName = "Settings/Session Settings")]
    public class SessionSettings : ScriptableObject
    {
        public enum SessionType
        {
            Training,
            Testing
        }
        
        public enum CueType
        {
            Neutral,
            FeatureBased,
            StimulusBased
        }

        public enum FeedbackType
        {
            Directional,
            Locational
        }

        public SessionType sessionType;
        public CueType cueType;
        public FeedbackType feedbackType;
        public int numTrials;
        public float fixationTime;
        public float fixationDotRadius;
        public Color skyColor;
        public float stimulusDensity;
        public float dotSize;
        public float outerStimulusRadius;
        public float outerStimulusNoisePercentage;
        public float innerStimulusRadius;
        public float innerStimulusSpawnRadius;
        public float innerStimulusNoisePercentage;
        public float outerStimulusDuration;
        public float innerStimulusDuration;
        public float stimulusDepth;
        public float interTrialDelay;
        public float stimulusSpacing;
        
        public List<float> coherenceStaircase;
        public int staircaseIncreaseThreshold;
        public int staircaseDecreaseThreshold;
        
        public int regionSlices;
        public bool flipRegions;

        public bool coarseAdjustEnabled;
        public List<float> choosableAngles;

        public float attentionCueDuration;
        public float attentionCueDepth;
        public float attentionCueDistance;
        public float pulseFrequency;
        public float sampleRate;
        public float angleErrorTolerance;
        public float positionErrorTolerance;
        public bool positionStaircaseEnabled;
        public bool directionStaircaseEnabled;
        public float fixationErrorTolerance;
        public float minDotLifetime;
        public float maxDotLifetime;
        public bool buddyDotsEnabled;
        public float outerStimulusStart;
        public float innerStimulusStart;
        public float attentionCueStart;

        public float inputStart;
        public float inputDuration;
        public float fixationBreakCheckStart;
        public float fixationBreakCheckDuration;

        public float headFixationDistanceErrorTolerance;
        public float headFixationAngleErrorTolerance;
        public float headFixationStart;
        public float headFixationDuration;

        public bool timeoutIsFailure;

        // IMPORTANT: Any changes made in this function should be cross-checked with both the corresponding JSON
        // and the UXF data-points collection
        public void LoadFromUxfJson()
        {
            var sessionSettingsDict = Session.instance.settings.GetDict("SessionSettings");
            
            sessionType = ParseSessionType((string) Session.instance.participantDetails["SessionType"]);
            cueType = ParseCueType((string) sessionSettingsDict["AttentionCueType"]);
            feedbackType = ParseFeedbackType((string) Session.instance.participantDetails["FeedbackType"]);
            numTrials = Convert.ToInt32(sessionSettingsDict["NumTrials"]);
            fixationTime = Convert.ToSingle(sessionSettingsDict["FixationTimeSeconds"]);
            fixationDotRadius = Convert.ToSingle(sessionSettingsDict["FixationDotRadiusDegrees"]);
            skyColor = ParseColor((List<object>) sessionSettingsDict["SkyColor"]);
            stimulusDensity = Convert.ToSingle(sessionSettingsDict["StimulusDensity"]);
            minDotLifetime = Convert.ToSingle(sessionSettingsDict["MinDotLifetimeSeconds"]);
            maxDotLifetime = Convert.ToSingle(sessionSettingsDict["MaxDotLifetimeSeconds"]);
            dotSize = Convert.ToSingle(sessionSettingsDict["StimulusDotSizeArcMinutes"]);
            outerStimulusRadius = Convert.ToSingle(sessionSettingsDict["OuterStimulusRadiusDegrees"]);
            outerStimulusNoisePercentage = Convert.ToSingle(sessionSettingsDict["OuterStimulusNoisePercentage"]);
            innerStimulusNoisePercentage = Convert.ToSingle(sessionSettingsDict["InnerStimulusNoisePercentage"]);
            innerStimulusRadius = Convert.ToSingle(sessionSettingsDict["InnerStimulusRadiusDegrees"]);
            innerStimulusSpawnRadius = Convert.ToSingle(sessionSettingsDict["InnerStimulusSpawnRadius"]);
            outerStimulusStart = Convert.ToSingle(sessionSettingsDict["OuterStimulusStartMs"]);
            outerStimulusDuration = Convert.ToSingle(sessionSettingsDict["OuterStimulusDurationMs"]);
            innerStimulusStart = Convert.ToSingle(sessionSettingsDict["InnerStimulusStartMs"]);
            innerStimulusDuration = Convert.ToSingle(sessionSettingsDict["InnerStimulusDurationMs"]);
            stimulusDepth = Convert.ToSingle(sessionSettingsDict["StimulusDepthMeters"]);
            interTrialDelay = Convert.ToSingle(sessionSettingsDict["InterTrialDelaySeconds"]);
 
            regionSlices = Convert.ToInt32(sessionSettingsDict["TotalRegionSlices"]);
            flipRegions = Convert.ToBoolean(sessionSettingsDict["FlipRegions"]);
            
            coherenceStaircase = ParseFloatList((List<object>) sessionSettingsDict["CoherenceStaircase"]);
            staircaseIncreaseThreshold = Convert.ToInt32(sessionSettingsDict["StaircaseIncreaseThreshold"]);
            staircaseDecreaseThreshold = Convert.ToInt32(sessionSettingsDict["StaircaseDecreaseThreshold"]);
            interTrialDelay = Convert.ToSingle(sessionSettingsDict["InterTrialDelaySeconds"]);
            
            stimulusSpacing = Convert.ToSingle(sessionSettingsDict["StimulusSpacingMeters"]);
            
            coarseAdjustEnabled = Convert.ToBoolean(sessionSettingsDict["CoarseAdjustment"]);
            choosableAngles = ParseFloatList((List<object>) sessionSettingsDict["ChoosableAngles"]);

            attentionCueStart = Convert.ToSingle(sessionSettingsDict["AttentionCueStartMs"]);
            attentionCueDuration = Convert.ToSingle(sessionSettingsDict["AttentionCueDurationMs"]);
            attentionCueDepth = Convert.ToSingle(sessionSettingsDict["AttentionCueDepth"]);
            attentionCueDistance = Convert.ToSingle(sessionSettingsDict["AttentionCueLengthDegrees"]);
            pulseFrequency = Convert.ToSingle(sessionSettingsDict["PulseFrequency"]);
            sampleRate = Convert.ToSingle(sessionSettingsDict["SampleRate"]);

            angleErrorTolerance = Convert.ToSingle(sessionSettingsDict["AngleErrorToleranceDegrees"]);
            positionErrorTolerance = Convert.ToSingle(sessionSettingsDict["PositionErrorToleranceDegrees"]);
            
            positionStaircaseEnabled = Convert.ToBoolean(sessionSettingsDict["EnableLocationalStaircase"]);
            directionStaircaseEnabled = Convert.ToBoolean(sessionSettingsDict["EnableDirectionalStaircase"]);
            
            fixationErrorTolerance = Convert.ToSingle(sessionSettingsDict["FixationErrorToleranceRadiusDegrees"]);
            buddyDotsEnabled = Convert.ToBoolean(sessionSettingsDict["EnableBuddyDots"]);
            
            inputStart = Convert.ToSingle(sessionSettingsDict["InputStartMs"]);
            inputDuration = Convert.ToSingle(sessionSettingsDict["InputDurationMs"]);
            fixationBreakCheckStart = Convert.ToSingle(sessionSettingsDict["FixationBreakCheckStartMs"]);
            fixationBreakCheckDuration = Convert.ToSingle(sessionSettingsDict["FixationBreakCheckDurationMs"]);

            headFixationDistanceErrorTolerance = Convert.ToSingle(sessionSettingsDict["HeadStabilityDistanceErrorToleranceM"]);
            headFixationAngleErrorTolerance =
                Convert.ToSingle(sessionSettingsDict["HeadStabilityAngleErrorToleranceDegrees"]);
            headFixationStart = Convert.ToSingle(sessionSettingsDict["HeadStabilityCheckStartMs"]);
            headFixationDuration = Convert.ToSingle(sessionSettingsDict["HeadStabilityCheckDurationMs"]);

            timeoutIsFailure = Convert.ToBoolean(sessionSettingsDict["FailOnTimeout"]);
        }

        private static List<float> ParseFloatList(IEnumerable<object> list)
        {
            return list.Select(Convert.ToSingle).ToList();
        }

        private static Color ParseColor(IReadOnlyList<object> color)
        {
            return new Color(
                Convert.ToSingle(color[0]),
                Convert.ToSingle(color[1]),
                Convert.ToSingle(color[2])
            );
        }

        private static SessionType ParseSessionType(string sessionTypeString)
        {
            switch (sessionTypeString)
            {
                case "Training":
                    return SessionType.Training;
                case "Testing":
                    return SessionType.Testing;
                default:
                    return SessionType.Training;
            }
        }
        
        private static CueType ParseCueType(string cueTypeString)
        {
            switch (char.ToLower(cueTypeString[0]))
            {
                case 'n':
                    return CueType.Neutral;
                case 'f':
                    return CueType.FeatureBased;
                case 's':
                    return CueType.StimulusBased;
                default:
                    return CueType.Neutral;
            }
        }
        
        private static FeedbackType ParseFeedbackType(string feedbackTypeString)
        {
            switch (char.ToLower(feedbackTypeString[0]))
            {
                case 'd':
                    return FeedbackType.Directional;
                case 'l':
                    return FeedbackType.Locational;
                default:
                    return FeedbackType.Directional;
            }
        }
    }
}
