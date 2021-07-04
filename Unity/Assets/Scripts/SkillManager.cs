using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    public List<Skill> skillList;
    private void Awake()
    {
        skillList = new List<Skill>(GetComponents<Skill>());
    }
}
