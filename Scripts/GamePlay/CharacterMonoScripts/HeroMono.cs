﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

/// <summary>
/// 英雄单位的Mono类
/// </summary>
public class HeroMono : CharacterMono{
    public HPViewModel HPViewModel;
    public AvatarViewModel avatarViewModel;

    public HeroModel HeroModel {
        get {
            return characterModel as HeroModel;
        }
    }

    public override void Update() {
        base.Update();
    }

    #region UI和Model之间的绑定
    protected override void Bind() {
        base.Bind();
        characterModel.HpValueChangedHandler += OnHpChanged;
        HeroModel.ExpChangedHandler += OnExpChanged;
        HeroModel.LevelChangedHandler += OnLevelChanged;
    }
    public void OnHpChanged(int oldHp, int newHp) {
        HPViewModel.Hp.Value = newHp;
    }
    public void OnExpChanged(int oldExp,int newExp) {

        //==========================================================================
        // 修改UI
        int expRate = Mathf.Clamp(Mathf.FloorToInt( ( (float)newExp / HeroModel.NextLevelNeedExp ) * 100 ),0,100);
        avatarViewModel.ExpRate.Value = expRate;

        //==============================
        // 当经验值满值时,修改等级
        if (expRate == 100) {
            HeroModel.Level += 1;
        }
    }
    public void OnLevelChanged(int oldLevel, int newLevel) {
        if (newLevel > oldLevel) {
            HeroModel.SkillPoint += HeroModel.skillPointGrowthPoint * (newLevel - oldLevel);
        }
        HeroModel.Exp = 0;
        avatarViewModel.Level.Value = newLevel;
    }
    #endregion
}

