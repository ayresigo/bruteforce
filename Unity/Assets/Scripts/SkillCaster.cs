using System.Collections.Generic;
using UnityEngine;

public class SkillCaster : MonoBehaviour
{
    //List of all spells
    public List<Skill> SkillList;

    private void Awake()
    {
        //Get all spells attached to this gameObject
        //You can fill this list in another way, it's just an example
        SkillList = new List<Skill>(GetComponents<Skill>());
    }

    void CastAllSpells()
    {
        //For each spell in spell list, call Cast
        //SkillList.ForEach(x => x.Cast());
    }
}