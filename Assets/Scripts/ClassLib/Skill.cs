using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StarFighter
{
    public class Skill
    {
        // Backend code for process CD and duration for the skill of a player fighter object ingame, this class can only initiated with json.
        // !This Class is only for processing the CD and active state for a skill, not used for making an actual skill!


        // Enum for skill state
        enum SkillState
        {
            onCD, // State on skill in CD
            onActive, // State on skill in active
            onInfinite, // State for skill with infinite duration, work as active state
            onReady // State on skill finished CD and waiting for activating (some multi-charge skill may not need this)
        }

        public float skillCD = 0f;
        public float skillDuration = 0f;
        public int charge = 0;
        public bool isInfiniteDuration = false;
        public bool disabled = false;
        public bool isNeedReadyText = false;

        SkillState state = SkillState.onCD;
        float curSkillCD = 0f;
        float curSkillDuration = 0f;
        int curCharge = 0;

        // Initialize needed non-public variable after object is constructed (!Call this method after JsonUtility!) 
        public void initialize()
        {
            state = SkillState.onCD;
            curSkillCD = skillCD;
            curSkillDuration = 0f;
            curCharge = 0;
        }

        // Method called on each fixedUpdate for count down the CD and duration time while in CD or active state
        public void countSkill(float deltaCD)
        {
            // Check if the skill is disabled before processing datas
            if (!disabled)
            {
                switch (state)
                {
                    // Count skill CD time on CD state
                    case SkillState.onCD:
                        if (curSkillCD > 0)
                        {
                            // deltaCD can used for special skill trigger such as Attck Count or Hit Count
                            curSkillCD -= deltaCD;
                        }
                        else
                        {
                            curCharge += 1;
                            // Change to Ready state when finished all charge's CD
                            if (curCharge == charge)
                            {
                                curSkillCD = skillCD;
                                state = SkillState.onReady;
                            }
                            // Reset CD when have charge remain
                            else
                            { 
                                curSkillCD -= deltaCD;
                                curSkillCD = skillCD + curSkillCD;
                            }
                        }
                        break;
                    // Count skill duration time on active state
                    case SkillState.onActive:
                        if (curSkillDuration > 0)
                        {
                            curSkillDuration -= Time.fixedDeltaTime;
                        }
                        // Change to CD state after skill duration is run out
                        else
                        {
                            state = SkillState.onCD;
                        }
                        break;
                }
            }
        }

        public bool isOnCD()
        {
            return state == SkillState.onCD;
        }

        public bool isOnReady()
        {
            return state == SkillState.onReady;
        }

        public bool isOnActive()
        {
            return state == SkillState.onActive;
        }

        public bool isOnInfinite()
        {
            return state == SkillState.onInfinite;
        }

        public bool hasAvaliableCharge()
        {
            return curCharge > 0;
        }

        // Method for trun skill state to active (usually called in the skill gameObject)
        public void activateSkill()
        {
            if (!isInfiniteDuration)
            {
                state = SkillState.onActive;
            }
            else
            {
                state = SkillState.onInfinite;
            }
            curSkillDuration = skillDuration;
            curCharge -= 1;
        }

        // Method for trun skill state to CD (usually called in the skill gameObject)
        public void deactivateSkill()
        {
            if(state == SkillState.onActive)
            {
                state = SkillState.onCD;
            }
        }

        // Method for return the current charge of the skill
        public int getCurCharge()
        {
            return curCharge;
        }

        // Method for return the skill CD time percentage (usually used for SPBar UI)
        public float getCDPercentage()
        {
            return Mathf.Clamp(1 - (curSkillCD / skillCD), 0, 1);
        }

        // Method for return the skill duration time percentage (usually used for SPBar UI)
        public float getDurationPercentage()
        {
            return Mathf.Clamp(curSkillDuration / skillDuration, 0, 1);
        }
    }
}
