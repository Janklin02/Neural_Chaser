# [Neural Chaser]
[Simple racing machine learning model.]
[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://
opensource.org/licenses/MIT)
## 📖 About The Project
This repository contains the Unity game project **[Neural Chaser]**.
Key features include Throttle mechanics, Dynamic turning mechanics and the ability to race against a machine learning race driver. This
project aims to deliver a fun fast paced and intense racing experience.
<img src="images/default_project_showcase.png" width="512" alt="Project Showcase
Image or Video">
---
## 📖 Getting Started
Follow these instructions to get a copy of the project up and running on your local
machine for development and testing.
### Prerequisites
* **Unity Hub** installed.
* **Unity Editor** ([Specify Your Unity Version, e.g., `6.6.6 LTS`] or newer). You
can install it via Unity Hub.
* **Git** (for cloning the repository).
* An IDE for C# (like **Visual Studio** or **Visual Studio Code** with Unity
integration).
### Installation
1. **Clone the repository:**
```sh
git clone
[https://github.com/your-username/your-repository.git](https://github.com/your-
username/your-repository.git)
```
(Alternatively, for private collaboration: or contact `<email>` to be added as
a contributor.)
2. **Open the project in Unity Hub:**
* Launch Unity Hub.
* Click "Open" or "Add project from disk".
* Navigate to the cloned `your-repository` folder and select it.
3. **Open the project in the Unity Editor:**
* Once added, click on the project name in Unity Hub to open it in the Unity
Editor.
* Unity will import assets and compile scripts. This might take a few minutes
the first time.
4. **Open the main scene:**
* Once the editor is open, locate the main scene file (e.g.,
`Assets/Scenes/Menu.unity`) in the Project window and double-click it to open.
---
## Features✨
This project includes the following main game features:
### Feature 1: [Dynamic Turning and Throttle System]
* **Description:** [Steering Strength and throttle are interlinked, the harder you press on the throttle the less you're able to turn the car creating a real feeling driving experience]
* **Characteristics:**
* [Uses floating point variables to determine the acceleration"].
* [Uses Int based variable counters to alter the speed at which you can accelerate].
* [Ability to turn is based on the amount of throttle given to the car, reducing with higher throttle values].
<img src="images/feature_1_image.png" width="512" alt="Image showcasing Feature 1">
### Feature 2: [Racing against fernando the Machine Learning Racer]
* **Description:** [Fernando is a little dumb but he works and can do a lap which is what's important, he might not be good but he can improve].
* **Characteristics:**
* [e.g., "Ensures replayability with varied objectives and rewards."].
* [e.g., "Integrates with the narrative system to create emergent storytelling
opportunities."].
* [e.g., "Quests are displayed in a UI journal and tracked on the world map."].
<img src="images/feature_2_image.png" width="512" alt="Image showcasing Feature 2">
---
## Usage
After installation and opening the project in Unity:
Press the Play button in the Unity Editor to run the game, typically starting from
the `Menu.unity` scene.
Key scene files include:
* `Assets/Scenes/Menu.unity`: Main menu and start screen.
* `Assets/Scenes/Game.unity`: Main gameplay scene.
* `Assets/Scenes/Credits.unity`: Displays the game credits.
Select GameObjects in the Hierarchy (e.g., 'Player', 'LevelManager') to view and
modify their properties in the Inspector window.
---
## FAQ❓
* **"I opened the project, but [specific problem, e.g., 'nothing happens when I
press Play' or 'I see errors in the Console']."**
* **Check the Console:** Look for error messages in Unity's Console window
(Window > General > Console). These often indicate missing scripts, incorrect
configurations, or compilation issues.
* **Script References:** Ensure all public script fields in the Inspector that
expect a reference (e.g., to another GameObject, Prefab, or Component) are
correctly assigned.
* **Unity Version:** Double-check that you are using a compatible Unity Editor
version as specified in the Prerequisites.
---
## 📖 Branches
This repository uses the following branches:
* **`main`**: [Has all the stuff advertised in the features point]
## 📖 Contributing
The team is open to contributions and feedback.
If you have a suggestion that would make this better, please fork the repo and
create a pull request. You can also simply open an issue with the tag
"enhancement".
Don't forget to give the project a star! Thanks again!
1. Fork the Project
2. Create your Feature Branch (`git checkout -b feature/AmazingFeature`)
3. Commit your Changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the Branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request
---
## 📖 License
Distributed under the MIT License. See `LICENSE.txt` for more information.
*(Consider adding a LICENSE.txt file with the MIT License text to your
repository).*
---
## 📖 Contact
Oscar Janke
- [oscarjanke@outlook.com]
Project Link:
https://github.com/Janklin02/Neural_Chaser/tree/main
---
## 📖 Acknowledgements
* This project is associated with the [Griffith Film
School](https://www.griffith.edu.au/arts-education-law/griffith-film-school) -
[Bachelor of Games Design and
Production](https://www.griffith.edu.au/study/degrees/bachelor-of-game-design-and-
production-1697)
* Special thanks to [Dr Justin Carter](https://experts.griffith.edu.au/37511-
justin-carter), [Dr Zac Fitz-Walter](https://experts.griffith.edu.au/35044-zac-
fitzwalter), [Dr Josh Hall](https://experts.griffith.edu.au/41263-joshua-hall),
[Henry Sun](https://www.linkedin.com/in/henrysunportfolio/)
