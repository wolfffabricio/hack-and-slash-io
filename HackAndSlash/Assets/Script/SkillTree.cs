using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillTree : MonoBehaviour
{
    List<Skill> skills = new List<Skill>();

    private void Awake()
    {
        //TODO - Puxar essas skills de um txt
        CreateSkills();
    }

    void CreateSkills()
    {
        int[] requirement;

        Skill s = new Skill("Vida 1", "Essa skill aumenta a sua capacidade de se manter vivo.", 1);
        skills.Add(s);

        s = new Skill("Velocidade 1", "Essa skill aumenta a sua velocidade de movimento.",1);
        skills.Add(s);

        s = new Skill("Força 1", "Essa skill aumenta o seu dano.",1);
        skills.Add(s);

        requirement = new int[1] { 0 };
        s = new Skill("Vida 2", "Essa skill aumenta a sua capacidade de se manter vivo.", 2,false, requirement);
        skills.Add(s);

        requirement = new int[1] { 1 };
        s = new Skill("Velocidade 2", "Essa skill aumenta a sua velocidade de movimento.", 2, false, requirement);
        skills.Add(s);

        requirement = new int[1] { 2 };
        s = new Skill("Força 2", "Essa skill aumenta o seu dano.", 2, false, requirement);
        skills.Add(s);
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public Skill GetSkill(int i)
    {
        return skills[i];
    }
}
