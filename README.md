# UniRandomErrorCauser

一定時間ごとにわざとエラーを発生させるコンポーネント

## 使用例

```cs
using Kogane;
using UnityEngine;

public class Example : MonoBehaviour
{
    private void Start()
    {
        // 必要であれば発生するエラーをカスタマイズできる
        // RandomErrorCauser.OnError = () => Debug.LogError( "ピカチュウ" );
    }

    private void Update()
    {
        if ( Input.GetKeyDown( KeyCode.Z ) )
        {
            // 1秒ごとにエラーが発生するようにする
            RandomErrorCauser.Play( 1 );
        }
        else if ( Input.GetKeyDown( KeyCode.X ) )
        {
            // 0～1秒ごとにエラーが発生するようにする
            RandomErrorCauser.Play( () => Random.value );
        }
        else if ( Input.GetKeyDown( KeyCode.C ) )
        {
            // エラーの発生を停止する
            RandomErrorCauser.Stop();
        }
    }
}
```
