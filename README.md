# HistoryBoothApp
# Noah Overton
# Cason Kirschner

1. App Summary:


2. Contributions:

          Noah Overton: 
          
          Cason Kirschner:

3. Work Percentage:

          Noah Overton: 
          
          Cason Kirschner: 



GUI Photobooth Project Implementation notes:

1. Use MVVM architecture with data binding between the View and ViewModel

2. Use a third-party library for reading and writing mp3 metadata
          ONLY READ AND WRITE TO "ApplicationData.Current.LocalFolder.Path" (using Taglib?). I believe we used this same library in MyTunes.
          "I was helping a team today who was having permission troubles using Taglib to save metadata to an mp3 file.  An exception would be thrown when trying to save the file, complaining that the app didn't have permissions to write to the folder containing the mp3 file.  UWP apps have limited permissions to read and write files.  By default, they are only supposed to read and write to files in their ApplicationData.Current.LocalFolder directory.  To figure out what your app's LocalFolder directory is, you can output to the debug console the ApplicationData.Current.LocalFolder.Path.  Your app should be setup to read and write mp3 files to this directory only." - Dr. McCown
          
3. Store all mp3 files in ApplicationData.LocalFolder

4. All outstanding bugs should be included in your repo as an Issue.

5. Requirements:

    Users may be any age, Harding students or not.

    Must allow a user to record a memory, not to exceed 10 minutes.

    Before recording, user should enter their name, indicate yes/no if they were a Harding student, and decade story is from.

    User must give permission for 3 points: Acknowledge this is a research project, only share appropriate stories, made publicly available on History Booth.

    User should be made aware of 10 minute limitation.

    User should be able to start, stop, pause, and resume recording.

    User should be allowed to re-play their recording and to re-record if they choose.

    User should be able to delete the recording and not re-record.

    After recording, user should be able to select from a set of tags (other). Perhaps this requirement should be dropped and only the admin create the tags to decrease amount of work for user.

    Show length of recording while story is being recorded and warn the user when time is almost up.

    After recording and choosing tags, user should be able to submit the recording.  After submitting, the software resets for a new user.

    Each recording should be saved in an mp3 file on the computer's hard drive. The user's name, student status, and story decade should be stored as metadata in the mp3 file.

    The administration software should be notified when a new recording is created.

    An admin user should be able to enter a password and see all recordings on that machine along with metadata.

    Admin user should be able to change the password.

    Admin user can logout so regular users can record again.  If the UI is inactive for more than 5 minutes, it should automatically logout the admin user.


MVVM NOTES (mostly from mvvm lecture):

          Create Model for representing User/User Data and
          an associated ViewModel (probably with same properties)
          that exposes properties to the UI. Use Data binding.
          
          Not using database, just holding mp3 with metadata
          in memory until recording is completed.
          
          Logic for mp3 code is in the MODEL. ViewModel won't know
          how to write to mp3, only how to instantiate model and expose
          its data to the UI.
          
          Admin model for holding info on admin with associated viewmodel.
          Observable collection or list for available recordings (expose to view).
