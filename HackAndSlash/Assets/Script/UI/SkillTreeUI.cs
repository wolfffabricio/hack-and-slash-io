using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillTreeUI : MonoBehaviour
{
    User user;
    Text textUserMoney;
    Text textSkillPrice;
    Text textSkillName;
    Text textSkillDescription;
    Button buySkillButton;

    Button[] skillsButtons;
    Transform skillsParent;

    SkillTree skillTree;

    Skill selectedSkill;
    private void Awake()
    {
        user = GameObject.Find("User").GetComponent<User>();
        textUserMoney = GameObject.Find("TextUserMoney").GetComponent<Text>();
        textSkillPrice = GameObject.Find("TextSkillPrice").GetComponent<Text>();
        textSkillName = GameObject.Find("TextSkillName").GetComponent<Text>();
        textSkillDescription = GameObject.Find("TextSkillDescription").GetComponent<Text>();
        buySkillButton = GameObject.Find("BuySkillButton").GetComponent<Button>();

        skillTree = GameObject.Find("SkillTree").GetComponent<SkillTree>();

        skillsParent = GameObject.Find("SkillsParent").transform;

        buySkillButton.interactable = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        skillsButtons = new Button[skillsParent.childCount];
        for (int i = 0; i < skillsParent.childCount; i++)
        {
            skillsButtons[i] = skillsParent.GetChild(i).GetComponent<Button>();
        }

        CheckBuyableSkills();

        UpdateMoney();
    }

    void CheckBuyableSkills()
    {
        for (int i = 0; i < skillsParent.childCount; i++)
        {
            skillsButtons[i] = skillsParent.GetChild(i).GetComponent<Button>();

            Skill skill = skillTree.GetSkill(i);

            if (skill.IsEquiped==false)
            {
                if(checkRequiredSkills(skill))
                {
                    skillsButtons[i].interactable = true;
                }
                else
                {
                    skillsButtons[i].interactable = false;
                }
            }
            else
            {
                skillsButtons[i].interactable = false;
            }
        }
    }

    bool checkRequiredSkills(Skill skill)
    {
        if (skill.UnlockRequirement == null)
        {
            return true;
        }
        else
        {
            foreach (int requirement in skill.UnlockRequirement)
            {
                if(skillTree.GetSkill(requirement).IsEquiped==false)
                {
                    return false;
                }
            }
        }
        return true;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void UpdateMoney()
    {
        textUserMoney.text = "Dinheiro: " + user.Gold.ToString();
    }

    public void BackButton()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }

    public void SelectSkillButton(int skillNumber)
    {
        selectedSkill = GameObject.Find("SkillTree").GetComponent<SkillTree>().GetSkill(skillNumber - 1);

        textSkillPrice.text = "Preço da Habilidade:" + selectedSkill.Price;
        textSkillName.text = selectedSkill.SkillName;
        textSkillDescription.text = selectedSkill.Description;

        buySkillButton.interactable = true;
    }

    public void BuySkillButton()
    {
        if (user.Gold >= selectedSkill.Price)
        {
            user.SpendGold(selectedSkill.Price);
            user.AddActiveSkill(selectedSkill);

            UpdateMoney();
            CheckBuyableSkills();

            buySkillButton.interactable = false;
        }
    }
}
