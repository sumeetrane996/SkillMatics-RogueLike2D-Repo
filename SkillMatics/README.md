#RougeLike2D-Assigment

##Task-1 :
1. created a prefab 'BoardLevel_1' added BoardManager Script.
2. Value specified in inspector will apply for that level
3. Create many such levels & drag into LevelBoardList in GameManager
4. With this designer can add as many custom levels

##Task-2 :
1. In prefab 'BoardLevel_1' with added BoardManager Script you can specify enemy count in each level through inspector.
2. Position of Player & Exit can be specified through inspector at each level.
3. If position of Player & Exit is inValid or same it would throw a warning & reset to default position

##Task-3 :
1. The Food Cap Functionality has been added on GameManager with MaxCapacity & Message field in inspector.
2. Designer can change as per preference.

##Task-4 :
1. Implemented Food Health Bar at top left which shows health based on current Food count.
2. Health Bar is loosely Coupled to Player. Irrespective of wether HealthBaar is enabled/disabled from Scene,the code & game flow would work fine.

##Task-5 :
1. Implemented Score board which displays **Current Score** & **Best Score** based on levels completed.
2. **Best Score** is the all time score & the data of it is stored on System locally & loaded when required.

##Task-6 :

##Optimization List :
1. SoundManager code can be optimized
2. Instead of adding AudioSource on respective Object can add it dynamically on RunTime.
3. Use of Public variables can be cut down, instead can use [SerializableField] with internal access specifier,Thereby allowing member variables to be less accessible & highly protected for alteration.
4. On every new level the Scene reloading can be prevented instead can Unload/Load and position prefabs[Less Time Consuming].
