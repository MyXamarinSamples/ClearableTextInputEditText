using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Support.Design.Widget;
using Android.Util;
using Android.Text;
using Java.Lang;
using Android;
using Android.Graphics.Drawables;
using Android.Content.Res;
using Android.Graphics;

namespace Metanoy
{
    public class ClearableTextInputEditText : TextInputEditText, ITextWatcher
    {

        private static readonly int DEFAULT_CLEAR_ICON_RES_ID = Resource.Drawable.ic_clear;
        
        private Drawable mClearIconDrawable;
        private bool mIsClearIconShown = false;
        private bool mClearIconDrawWhenFocused = true;

        public ClearableTextInputEditText(Context context) : base(context)
        {

        }

        public ClearableTextInputEditText(Context context, IAttributeSet attrs) : base(context, attrs)
        {

        }

        public ClearableTextInputEditText(Context context, IAttributeSet attrs, int defStyleAttr) : base(context, attrs, defStyleAttr)
        {
            Init(attrs, defStyleAttr);
        }

        protected ClearableTextInputEditText(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer)
        {

        }
        private void Init(IAttributeSet attrs, int defStyleAttr)
        {
            TypedArray t = Context.ObtainStyledAttributes(attrs, new int[] { Resource.Attribute.clearIconDrawable, Resource.Attribute.clearIconDrawWhenFocused }, defStyleAttr, 0);

            if (t.HasValue(0))
            {
                mClearIconDrawable = t.GetDrawable(0);

                if (mClearIconDrawable != null)
                {
                    mClearIconDrawable.Callback = this;
                }
            }

            mClearIconDrawWhenFocused = t.GetBoolean(1, true);

            t.Recycle();
        }

        void ITextWatcher.AfterTextChanged(IEditable s)
        {
            //throw new NotImplementedException();
        }

        void ITextWatcher.BeforeTextChanged(ICharSequence s, int start, int count, int after)
        {
            //throw new NotImplementedException();
        }

        public override IParcelable OnSaveInstanceState()
        {
            //return base.OnSaveInstanceState();
            IParcelable superState = base.OnSaveInstanceState();
            return mIsClearIconShown ? new ClearIconSavedState(superState, true) : superState;
        }

        public override void OnRestoreInstanceState(IParcelable state)
        {
            //base.OnRestoreInstanceState(state);
            if (!(state is ClearIconSavedState))
            {
                base.OnRestoreInstanceState(state);
                return;
            }

            ClearIconSavedState savedState = (ClearIconSavedState)state;
            base.OnRestoreInstanceState(savedState.SuperState);
            mIsClearIconShown = savedState.IsClearIconShown();
            ShowClearIcon(mIsClearIconShown);
        }

        void ITextWatcher.OnTextChanged(ICharSequence s, int start, int before, int count)
        {
            if (HasFocus)
            {
                ShowClearIcon(!TextUtils.IsEmpty(s));
            }
        }

        protected override void OnFocusChanged(bool gainFocus, [GeneratedEnum] FocusSearchDirection direction, Rect previouslyFocusedRect)
        {
            ShowClearIcon((!mClearIconDrawWhenFocused || gainFocus) && !TextUtils.IsEmpty(Text));
            base.OnFocusChanged(gainFocus, direction, previouslyFocusedRect);
        }

        public override bool OnTouchEvent(MotionEvent e)
        {
            if (IsClearIconTouched(e))
            {
                Text = null;
                e.Action = MotionEventActions.Cancel;
                ShowClearIcon(false);
                return false;
            }

            return base.OnTouchEvent(e);
        }

        private bool IsClearIconTouched(MotionEvent e)
        {
            if (!mIsClearIconShown)
            {
                return false;
            }

            int touchPointX = (int)e.GetX();
            int widthOfView = Width;
            int compoundPaddingRight = CompoundPaddingRight;

            return touchPointX >= widthOfView - compoundPaddingRight;
        }

        private void ShowClearIcon(bool show)
        {
            if (show)
            {
                //show icon on the right
                if (mClearIconDrawable != null)
                {
                    SetCompoundDrawablesWithIntrinsicBounds(null, null, mClearIconDrawable, null);
                }
                else
                {
                    SetCompoundDrawablesWithIntrinsicBounds(0, 0, DEFAULT_CLEAR_ICON_RES_ID, 0);
                }
            }
            else
            {
                //remove icon
                SetCompoundDrawables(null, null, null, null);
            }
            mIsClearIconShown = show;
        }

        protected class ClearIconSavedState : BaseSavedState, IParcelableCreator
        {
            public Java.Lang.Object CreateFromParcel(Parcel source)
            {
                return new ClearIconSavedState(source);
            }

            public Java.Lang.Object[] NewArray(int size)
            {
                return new ClearIconSavedState[size];
            }

            private readonly bool mIsClearIconShown;

            private ClearIconSavedState(Parcel source) : base(source)
            {
                mIsClearIconShown = source.ReadByte() != 0;
            }

            public ClearIconSavedState(IParcelable superState, bool isClearIconShown) : base(superState)
            {
                mIsClearIconShown = isClearIconShown;
            }

            public override void WriteToParcel(Parcel dest, [GeneratedEnum] ParcelableWriteFlags flags)
            {
                base.WriteToParcel(dest, flags);
                dest.WriteByte((sbyte)(mIsClearIconShown ? 1 : 0));
            }

            public bool IsClearIconShown()
            {
                return mIsClearIconShown;
            }
        }
    }
}