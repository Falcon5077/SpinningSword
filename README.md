# SpinningSword 스피닝소드
### [웹에서 플레이 해보기](https://falcon5077.github.io/WebglTest/SpinningSword/)
### [스피닝소드 구글 플레이스토어](https://play.google.com/store/apps/details?id=com.dibs.SpinningSword) 
<img alt="image" src="https://github.com/Falcon5077/SpinningSword/assets/32628758/5176a317-a999-43a2-8302-c546db73606f" width="25%" height="25%">

킬링타임용 하이퍼캐쥬얼 게임입니다. <br>
플래피버드 제작자는 사람들의 시간을 너무 많이 뺏어서 후회했다고 합니다.

저도 만든걸 후회할만큼 재밌는 게임을 만들고 싶었습니다. <br>
아직 기획한 만큼 덜 만들어졌기 때문에 지속적으로 업데이트 하겠습니다.  감사합니다.

---

<div style="display: flex; justify-content: space-between;">
  <img src="https://github.com/Falcon5077/SpinningSword/assets/32628758/9df63978-f183-46ee-bf0a-a5d1d68919a5" width="30%" height="30%">
  <img src="https://github.com/Falcon5077/SpinningSword/assets/32628758/ef38af06-b199-44ea-a9c7-7f18ef784cc3" width="30%" height="30%">
</div>

### 스킨 시스템 구현

```csharp
public class SkinSelect : MonoBehaviour
{
    public int selectIndex = 0;
    public GameObject selectedSprite;
    public RectTransform ScrollContent;

    void Start()
    {
        // 1. 시작할 때 PlayerPrefs에 저장되어 있던 선택 인덱스를 가져옴
        selectIndex = PlayerPrefs.GetInt("selectIndex",0);

        // 2. 이미지게터로 현재 칼 이미지를 선택한 인덱스로 바꿈
        ImageGetter.instance.setSkin(selectIndex);
    }

    public void selectKnife(GameObject knife)
    {
        // 3. 스킨 선택 시 체크박스 이미지의 부모를 바꿈 (연출)
        selectedSprite.transform.SetParent(knife.transform.parent);
        selectedSprite.SetActive(true);
        selectedSprite.GetComponent<RectTransform>().anchoredPosition3D = new Vector3(0,215,0);

        // 4. 선택한 스킨의 이름 == 인덱스
        selectIndex = knife.transform.parent.name;

        // 5. 이미지게터로 현재 칼 이미지를 선택한 인덱스로 바꿈
        ImageGetter.instance.setSkin(selectIndex);

        // 6. PlayerPrefs에 선택한 인덱스 저장
        PlayerPrefs.SetInt("selectIndex",selectIndex);
    }
}
```
