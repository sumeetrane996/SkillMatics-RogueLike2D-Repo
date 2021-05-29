#RougeLike2D-Assigment

##Task-1
-created a prefab 'BoardLevel_1' added BoardManager Script.
-Value specified in inspector will apply for that level
-Create many such levels & drag into LevelBoardList in GameManager
-With this designer can add as many custom levels

##Task-2
-In prefab 'BoardLevel_1' with added BoardManager Script you can specify enemy count in each level through inspector.
-Position of Player & Exit can be specified through inspector at each level.
-If position of Player & Exit is inValid or same it would throw a warning & reset to default position

##Task-3
-The Food Cap Functionality has been added on GameManager with MaxCapacity & Message field in inspector.
-Designer can change as per preference.

##Task-4
-Implemented Food Health Bar at top left which shows health based on current Food count.
-Health Bar is loosely Coupled to Player. Irrespective of wether HealthBaar is enabled/disabled from Scene,the code & game flow would work fine.

##Task-5 
-Implemented Score board which displays **Current Score** & **Best Score** based on levels completed.
-**Best Score** is the all time score & the data of it is stored on System locally & loaded when required.

##Task-6

##Optimization List
-SoundManager code can be optimized
-Instead of adding AudioSource on respective Object can add it dynamically on RunTime.
-Use of Public variables can be cut down, instead can use [SerializableField] with internal access specifier,Thereby allowing member variables to be less accessible & highly protected for alteration.
-On every new level the Scene reloading can be prevented instead can Unload/Load and position prefabs[Less Time Consuming].
