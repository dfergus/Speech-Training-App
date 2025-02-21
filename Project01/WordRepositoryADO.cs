using System;
using System.Collections.Generic;
using System.IO;

namespace Project01 {
	public class WordRepositoryADO {
		WordDatabase db = null;
		protected static string dbLocation;		
		protected static WordRepositoryADO me;		

		static WordRepositoryADO ()
		{
			me = new WordRepositoryADO();
		}

		protected WordRepositoryADO ()
		{
			// set the db location
			dbLocation = DatabaseFilePath;

			// instantiate the database	
			db = new WordDatabase(dbLocation);
		}

		public static string DatabaseFilePath {
			get { 
				var sqliteFilename = "WordDatabase.db3";
				#if NETFX_CORE
				var path = Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path, sqliteFilename);
				#else

				#if SILVERLIGHT
				// Windows Phone expects a local path, not absolute
				var path = sqliteFilename;
				#else

				#if __ANDROID__
				// Just use whatever directory SpecialFolder.Personal returns
				string libraryPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal); ;
				#else
				// we need to put in /Library/ on iOS5.1 to meet Apple's iCloud terms
				// (they don't want non-user-generated data in Documents)
				string documentsPath = Environment.GetFolderPath (Environment.SpecialFolder.Personal); // Documents folder
				string libraryPath = Path.Combine (documentsPath, "..", "Library"); // Library folder
				#endif
				var path = Path.Combine (libraryPath, sqliteFilename);
				#endif

				#endif
				return path;	
			}
		}

		public static Word GetWord(int id)
		{
			return me.db.GetItem(id);
		}

		public static IEnumerable<Word> GetWords ()
		{
			return me.db.GetItems();
		}

		public static int SaveWord (Word item)
		{
			return me.db.SaveItem(item);
		}

		public static int DeleteWord(int id)
		{
			return me.db.DeleteItem(id);
		}

	}
}

