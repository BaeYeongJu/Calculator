## [설계 구조]

Model
 - MemoryManager
 class
  Action
  - Save
  - SaveAll
  - Load
  - Clear
  - Add
  - Subtract
  - history
View 
 - MainWindow.xaml
Controller
 - UIManager class
   : Button,Text
 - ModificationManager class
   Action
   - Percent
   - Delete
   - DeleteAll
   - Clear
   
   - Add
   - Subtract
   - Multiply
   - Divide
   - Result
   
   - DecimalPoint
   - Mathematics (0~9)
   - PlusOnMinus
   
   - Reciprocal
   - SquareRoot
   - Root
   
### [계산기 설계 ]

![계산기 설계 구조](https://user-images.githubusercontent.com/18066652/155834663-8a39b08d-1a38-4811-9342-4f87d5868c8f.PNG)