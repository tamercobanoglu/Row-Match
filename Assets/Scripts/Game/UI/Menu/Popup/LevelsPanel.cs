using UnityEngine;
using Game.UI.Buttons;
using System.Collections;
using DG.Tweening;

namespace Game.UI.Menu.Popup {
	public class LevelsPanel : MonoBehaviour, IButton {
		public ButtonType ButtonType { get { return _buttonType; } }
		public bool IsSelected { get { return _isSelected; } }

		private ButtonType _buttonType = ButtonType.LevelsPanel;
		private bool _isSelected = true;

		public Transform LevelsParent;
		[HideInInspector] public float SlidingDuration;

		private Vector3 _pos;
		private int _levelCount;
		private float _offsetY, _basePosY, _topPosY, _instantPosY, _time;

		[SerializeField] private float _acc = 1f;
		[SerializeField] private float _maxSpeed = 50f;

		public void Prepare(int levelCount) {
			SlidingDuration = 0.25f;

			_levelCount = levelCount;
			_basePosY = 3.5f;
			_topPosY = CalculateTopYPos(_levelCount);
		}

		public void Operate(Vector3 pos, TouchPhase touchPase) {

			switch (touchPase) {
				case TouchPhase.Began:
					Selected(pos);
					break;
				case TouchPhase.Moved:
					Moved(pos);
					break;
				case TouchPhase.Stationary:
					Stationary(pos);
					break;
				case TouchPhase.Ended:
					Released(pos);
					break;
				case TouchPhase.Canceled:
					Canceled(pos);
					break;
			}
		}

		public void Selected(Vector3 pos) {
			_isSelected = true;

			StopAllCoroutines();
			_instantPosY = pos.y;
			_offsetY = LevelsParent.position.y - pos.y;
		}

		public void Moved(Vector3 pos) {
			if (!_isSelected) return;

			if (_pos == pos) {
				_time = 0;
				return;
			}

			LevelsParent.position = new Vector3(
				LevelsParent.position.x, 
				pos.y + _offsetY, 
				LevelsParent.position.z);

			_pos = pos;
			_time += Time.deltaTime;
		}

		public void Stationary(Vector3 pos) {

		}

		public void Released(Vector3 pos) {
			if (!_isSelected) return;

			/// sliding process
			if (_time != 0) {
				StartCoroutine(Slide((pos.y - _instantPosY) / _time));
			}
			else if(_time == 0 && OutOfBounds()) {
				StartCoroutine(Return(pos.y - _instantPosY > 0 ? LimitPosType.Top : LimitPosType.Base));
			}

			_time = 0;
			_isSelected = false;
		}

		public void Canceled(Vector3 pos) {
			Released(pos);

			_isSelected = false;
		}

		IEnumerator Slide(float vel) {
			vel = LimitVelocity(vel);

			while (Mathf.Abs(vel) > _acc) {
				if (vel > 0) vel -= _acc;
				else vel += _acc;

				var pos = LevelsParent.position;
				pos.y += vel * Time.deltaTime;

				if (OutOfBounds(vel, pos.y)) {
					pos.y = vel > 0 ? _topPosY : _basePosY;
					vel = 0f;
				}

				LevelsParent.position = pos;

				yield return new WaitForFixedUpdate();
			}
		}

		IEnumerator Return(LimitPosType lpt) {
			bool isReached = false;
			var vel = _maxSpeed * (int)lpt;

			while (!isReached) {
				var pos = LevelsParent.position;
				pos.y += vel * Time.deltaTime;

				if (IsReturned(vel, pos.y)) {
					isReached = true;
					pos.y = vel < 0 ? _topPosY : _basePosY;
					vel = 0f;
				}

				LevelsParent.position = pos;
				yield return new WaitForFixedUpdate();
			}
		}

		public void ResetPanelPos() {
			LevelsParent.position = new Vector3(
				LevelsParent.position.x,
				_basePosY,
				LevelsParent.position.z);
		}

		public void SelfSlide(int levelIndex) {
			LevelsParent.DOLocalMoveY(CalculateSelfSlidingPosY(levelIndex), SlidingDuration);
		}

		#region Calculations
		private float CalculateTopYPos(int levelCount) {
			var levelCardImgHeight = 1.8f;
			var levelCardGap = 0.45f;
			var halfOfPanelHeight = 6.5f;
			/// parent go transform eliminates that gap
			/// var bottomGap = 0.5f;

			var extendedPanelHeight = levelCount * levelCardImgHeight + (levelCount - 1) * levelCardGap;
			var distance = extendedPanelHeight - (levelCardImgHeight / 2 + _basePosY + halfOfPanelHeight);

			return _basePosY + distance;
		}

		private float CalculateSelfSlidingPosY(int levelIndex) {
			var localBasePosY = _basePosY + 0.5f;
			var localTopPosY = _topPosY + 0.5f;
			var levelCardMargin = 2.25f;

			var distance = localBasePosY + levelIndex * levelCardMargin;
			distance = distance >= localTopPosY ? localTopPosY : distance;

			return distance;
		}

		private float LimitVelocity(float vel) {
			return Mathf.Abs(vel) >= _maxSpeed ?
				vel >= 0 ? _maxSpeed : -_maxSpeed : vel;
		}

		private bool OutOfBounds(float vel, float posY) {
			return vel > 0 ? posY >= _topPosY : posY <= _basePosY;
		}

		private bool OutOfBounds() {
			return LevelsParent.position.y < _basePosY || LevelsParent.position.y > _topPosY;
		}

		private bool IsReturned(float vel, float posY) {
			return vel < 0 ? posY <= _topPosY : posY >= _basePosY;
		}
		#endregion
	}

	public enum LimitPosType { 
		Base = 1,
		Top = -1
	}
}

