# To-Do List
## Major 

1. Flesh out todo
2. Fix CollapsibleChildStackPanel https://gist.github.com/tomtom-m/4df733e1fdb68291ed4a5100629c11c4
3. Implement naming scheme API, generic way to obtain values
    - Big thing is UWP time APIs or not
4. Implement screen recording via. ddagrab in ffmpeg or with our own desktop duplication solution. 
    - Custom solution may provide easier access to things such as HDR and bitrate settings, even if tedious
    - ddagrab is not in most ffmpeg builds yet
5. Implement dynamic audio/video quality settings dialogs based on known parameters to ffmpeg codecs (samplerate in opus? bitrate in most? 1-9 quality in vorbis?)
6. Implement ffmpeg in a convenient way; either ship our own build or a known good one, use some sort of libavcodec interop library, or find another solution outright
7. Properly serialize UI values to ffmpeg arg stream, if required
8. Port to WinUI 3 for easier use with libavcodec via. C++ or C#, unbound by the UWP container (initially thought UWP was a good idea, but WMF made it difficult)

9. Make new control (TextBox) with a fixed-size label to the right of it, in order to avoid inconsistent X positions of text boxes in vertical layouts
## Minor