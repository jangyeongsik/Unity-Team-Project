using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillPreset : MonoBehaviour
{
    //스킬들
    AnimationClip[] skills;

    int curSkillIdx = 0;
    int selectIdx = 0;

    // 1,2,3 번스킬 이미지 텍스트
    Image[] btnImages;
    Text[] btnText;

    //선택, 디폴트 색
    Color selectColor = new Color(1, 121/255, 121/255);
    Color defalt = new Color(1, 1, 1);
    Color DeActive = new Color(0, 0, 0);

    //스킬 프리셋 이미지
    Image[] skillImages;
    Text[] skillTexts;

    private void Awake()
    {
        curSkillIdx = 0;
        selectIdx = 0;
        if (btnText == null)
        {
            btnImages = new Image[3];
            btnText = new Text[3];
            int i = 0;
            foreach (Transform tr in transform.Find("Skills"))
            {
                btnImages[i] = tr.GetComponent<Image>();
                i++;
            }
        }
        skillImages = transform.Find("Skill Presets").GetComponentsInChildren<Image>();
        skillImages[selectIdx].color = selectColor;
        btnImages[curSkillIdx].color = selectColor;
    }

    private void Start()
    {
        skills = GameData.Instance.player.skill.skillClips;
        
    }

    private void OnEnable()
    {
        curSkillIdx = 0;
        selectIdx = 0;

        for (int i = 0; i < GameData.Instance.player.skillIdx.Length; ++i)
        {
            btnImages[i].sprite = skillImages[GameData.Instance.player.skillIdx[i]-1].sprite;
        }
        for(int i = 0; i < GameData.Instance.player.skillShop.Length; ++i)
        {
            if(!GameData.Instance.player.skillShop[i])
            {
                skillImages[i].color = DeActive;
                skillImages[i].raycastTarget = false;
            }
            else
            {
                skillImages[i].color = defalt;
                skillImages[i].raycastTarget = true;
            }
        }

        skillImages[selectIdx].color = selectColor;
        btnImages[curSkillIdx].color = selectColor;
    }

    //현재 무슨버튼 눌렀는지 바꿔줌
    public void SelectSkill(int idx)
    {
        SoundManager.Instance.OnPlayOneShot(SoundKind.Sound_UISound, "메뉴클릭2");
        skillImages[selectIdx].color = defalt;
        skillImages[idx].color = selectColor;
        selectIdx = idx;
    }

    //선택한 스킬 빨간칠
    public void SelectSkillBtn(int idx)
    {
        SoundManager.Instance.OnPlayOneShot(SoundKind.Sound_UISound, "메뉴클릭2");
        btnImages[curSkillIdx].color = defalt;
        btnImages[idx].color = selectColor;
        curSkillIdx = idx;
    }

    //선택한 애니메이션 적용
    public void EquipSkill()
    {
        SoundManager.Instance.OnPlayOneShot(SoundKind.Sound_UISound, "메뉴클릭2");
        //이미 장착한게 있는지 확인
        for (int i = 0; i < GameData.Instance.player.aniList.Count; ++i)
        {
            if (i == curSkillIdx) continue;
            if (GameData.Instance.player.aniList[i] == skills[selectIdx]) return;
        }
        GameData.Instance.player.aniList[curSkillIdx] = skills[selectIdx];
        btnImages[curSkillIdx].sprite = skillImages[selectIdx].sprite;
        GameData.Instance.player.skillIdx[curSkillIdx] = selectIdx+1;
    }

    public void Cancle()
    {
        SoundManager.Instance.OnPlayOneShot(SoundKind.Sound_UISound, "메뉴클릭2");
        gameObject.SetActive(false);
    }

    //선택창이 닫힐때 스킬변경을 적용시킨다
    //게임데이터 참조때문에 쓸데없이 길어보일뿐입니다
    private void OnDisable()
    {
        GameData.Instance.player.applyList.Clear();
        for(int i = 0; i < GameData.Instance.player.orgList.Count; ++i)
        {
            GameData.Instance.player.applyList.Add(new KeyValuePair<AnimationClip, AnimationClip>(GameData.Instance.player.orgList[i], GameData.Instance.player.aniList[i]));
        }
        GameData.Instance.player.overrideController.ApplyOverrides(GameData.Instance.player.applyList);
        GameData.Instance.player.animator.runtimeAnimatorController = GameData.Instance.player.overrideController;

        btnImages[curSkillIdx].color = defalt;
        skillImages[selectIdx].color = defalt;
    }

}
