# Adaptive AI Combat System (Unity + XGBoost) ⚔️🤖

A 3D combat framework built in Unity featuring frame-accurate hitboxes and an adaptive enemy AI powered by an XGBoost machine learning model trained on player telemetry.

---

## 🎮 Overview

This project bridges traditional gameplay engineering with machine learning. Instead of relying solely on hardcoded Finite State Machines (FSMs), the enemy AI logs player combat patterns and uses an XGBoost classifier to predict incoming actions and dynamically alter its tactics in real time.

---

## 🛠️ Tech Stack & Features

* **Game Engine & Code:** Unity 3D, C# (Combat Mechanics, Animation Events, Hitbox/Hurtbox Detection)
* **Machine Learning:** Python, XGBoost (Gradient-Boosted Decision Trees)
* **Data Pipeline:** Telemetry logging system tracking player attack vectors, timing, and sequence choices.

---

## 🚀 Key Technical Highlights

* **Responsive Hitboxes:** Frame-accurate melee damage volumes synchronized directly with character animation clips.
* **Player Pattern Logging:** Captures real-time combat metrics (spacing, attack frequency, parry timing) to train model weights.
* **Predictive AI Behavior:** Seamlessly feeds ML predictions into the C# state machine, allowing the enemy to parry, dodge, or counter-attack based on player tendencies.
