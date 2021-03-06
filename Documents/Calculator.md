
## [요구 명세서]

### [요구 조건]
1. MVC 패턴을 사용해서 기술 개발
2. winform, wpf, uwp, unity 언어들 중에서 wpf를 사용할 예정입니다.
3. 계산기 구현 하기

### [개발 목적]
MVC 패턴을 활용한 Window 10 에서 제공해주는 계산기를 만들어 보자

### [기능]

- 기록 : 수식 전체 히스토리 기능
- 계산기 창 크기 조절 : 창 사이드 늘리면 기록, 메모리 기능 활성화
- 항상 위에 유지 : 창을 오른쪽 상단 바에 위치/원래 위치로 이동 하는 기능

- MC (모든 메모리 지우기)
- MR (메모리에서 저장된 값 불려오기)
- M+ (특정 값을 +의 값으로 기억)
- M- (특정 값을 -의 값으로 기억)
- MS (메모리 저장)
- M목록 (메모리 기록)

- % : 퍼센트 기능  ex) 100 x 10% 라고 하면 100 x 0.1 = 10
- CE : 현재 입력 지우기
- C : 모든 입력 지우기 (전체 삭제)
- +/- : -또는 +로 변환
- 1/x : 현재 숫자의 역수를 구한다. ex) 2->1/x -> 1/(2) = 0.5
- x^2 : 제곱근 기능 ex) 2 -> x^2 -> sqr(2) = 4
- 2√x : 루트 ex) 4->2√x -> √(4) = 2
- . : 소수점

- x : 곱셈 기능
- "-" : 빼기 기능
- "+" : 더하기 기능
- = : 대입 기능

#### 참조 할 사항)
*소숫점  16자리까지만 나옴


### [계산기 UI]

![계산기 구조](https://user-images.githubusercontent.com/18066652/155834698-ade86407-3e23-44bb-9a3e-a6ea27eee5e3.png)


### 참조 링크)
#### winform,wpf,uwp 차이점
https://m.blog.naver.com/mincoding/221707039355

#### mvc
https://bsnippet.tistory.com/13
https://m.blog.naver.com/jhc9639/220967034588
https://kayuse88.github.io/mvvm-example/