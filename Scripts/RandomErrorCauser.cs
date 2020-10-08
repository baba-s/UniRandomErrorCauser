using JetBrains.Annotations;
using System;
using System.Collections;
using UnityEngine;

namespace Kogane
{
	/// <summary>
	/// 定期的にわざとエラーを発生されるコンポーネント
	/// </summary>
	[DisallowMultipleComponent]
	public sealed class RandomErrorCauser : MonoBehaviour
	{
		//================================================================================
		// 変数(static)
		//================================================================================
		private static GameObject m_instance;

		//================================================================================
		// デリゲート(static)
		//================================================================================
		[NotNull] public static Action OnError { get; set; } = () => Debug.LogError( nameof( RandomErrorCauser ) );

		//================================================================================
		// 関数
		//================================================================================
		/// <summary>
		/// 指定された時間ごとにエラーを発生させる処理を開始します
		/// </summary>
		private void PlayImpl( Func<float> getIntervalSeconds )
		{
			StartCoroutine( DoPlayImpl( getIntervalSeconds ) );
		}

		/// <summary>
		/// 指定された時間ごとにエラーを発生させる処理をコルーチンで行います
		/// </summary>
		private IEnumerator DoPlayImpl( Func<float> getIntervalSeconds )
		{
			while ( true )
			{
				var seconds = getIntervalSeconds();

				yield return new WaitForSeconds( seconds );

				OnError();
			}
		}

		//================================================================================
		// 関数(static)
		//================================================================================
		/// <summary>
		/// 指定された時間ごとにエラーを発生させる処理を開始します
		/// </summary>
		public static void Play( float intervalSeconds )
		{
			Play( () => intervalSeconds );
		}

		/// <summary>
		/// 指定された時間ごとにエラーを発生させる処理を開始します
		/// </summary>
		public static void Play( Func<float> getIntervalSeconds )
		{
			if ( m_instance != null ) return;

			m_instance = new GameObject();
			DontDestroyOnLoad( m_instance );
			var causer = m_instance.AddComponent<RandomErrorCauser>();
			causer.PlayImpl( getIntervalSeconds );
		}

		/// <summary>
		/// 指定された時間ごとにエラーを発生させる処理を停止します
		/// </summary>
		public static void Stop()
		{
			if ( m_instance == null ) return;

			Destroy( m_instance );
			m_instance = null;
		}
	}
}