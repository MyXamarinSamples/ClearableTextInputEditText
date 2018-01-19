# ClearableTextInputEditText
A clearable TextInputEditText view for Xamarin Android

Function
---
![Usage](https://github.com/Metanoy/ClearableTextInputEditText/blob/master/screenshots/usage.gif)

- Add an icon to the right of TextInputEditText
- Clear text when the icon is touched
- Hide icon when EditText not focused
- You can define your own icon drawable

Usage
---
By default, the clear icon is a black material design icon called 'drawable/ic_clear'.

Default Clearable EditText:
```xml
<Metanoy.ClearableTextInputEditText
    p1:layout_width="match_parent"
    p1:layout_height="wrap_content"
    p1:id="@+id/clearableTextInputEditText1" />
```

Screenshot of demo:
![Default Icon](https://github.com/Metanoy/ClearableTextInputEditText/blob/master/screenshots/default-icon.png)

And you can define your own icon drawable by assigning drawable resource to `app:clearIconDrawable`:

Custom Clearable EditText:
```xml
<Metanoy.ClearableTextInputEditText
    p1:layout_width="match_parent"
    p1:layout_height="wrap_content"
    p1:id="@+id/clearableTextInputEditText1"
    app:clearIconDrawable="@drawable/ic_clear_grey_500_24dp"
    app:clearIconDrawWhenFocused="false" />
```

Screenshot of demo:
![Custom Icon](https://github.com/Metanoy/ClearableTextInputEditText/blob/master/screenshots/custom-icon.png)

You can use subclasses in the same way(just change the class name).
