using CrowdDJ.Interfaces;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CrowdDJ.DAL
{
    public class DALFactory
    {
        private static string assemblyName;
        private static Assembly dalAssembly;

        static DALFactory()
        {
            assemblyName = ConfigurationManager.AppSettings["DALAssembly"];
            dalAssembly = Assembly.Load(assemblyName);
        }

        public static IDataBase CreateDatabase()
        {
            return CreateDatabase(ConfigurationManager.ConnectionStrings["DefaultConnectionString"].ConnectionString);
        }

        public static IDataBase CreateDatabase(string connectionString)
        {
            string databaseClassName = assemblyName + ".Database";
            Type dbClass = dalAssembly.GetType(databaseClassName);

            return Activator.CreateInstance(dbClass,
              new object[] { connectionString }) as IDataBase;
        }

        public static IGuest CreateGuestDao(IDataBase database)
        {
            Type guestType = dalAssembly.GetType(assemblyName + ".GuestDAO");
            return Activator.CreateInstance(guestType, new object[] { database })
                     as IGuest;
        }

        public static IParty CreatePartyDao(IDataBase database)
        {
            Type partyType = dalAssembly.GetType(assemblyName + ".PartyDAO");
            return Activator.CreateInstance(partyType, new object[] { database })
                     as IParty;
        }
        public static IPartytweet CreatePartytweetDao(IDataBase database)
        {
            Type partytweetType = dalAssembly.GetType(assemblyName + ".PartytweetDAO");
            return Activator.CreateInstance(partytweetType, new object[] { database })
                     as IPartytweet;
        }
        public static IPlaylist CreatePlaylistDao(IDataBase database)
        {
            Type playlistType = dalAssembly.GetType(assemblyName + ".PlaylistDAO");
            return Activator.CreateInstance(playlistType, new object[] { database })
                     as IPlaylist;
        }
        public static ITrack CreateTrackDao(IDataBase database)
        {
            Type trackType = dalAssembly.GetType(assemblyName + ".TrackDAO");
            return Activator.CreateInstance(trackType, new object[] { database })
                     as ITrack;
        }
        public static ITracklist CreateTracklistDao(IDataBase database)
        {
            Type tracklistType = dalAssembly.GetType(assemblyName + ".TracklistDAO");
            return Activator.CreateInstance(tracklistType, new object[] { database })
                     as ITracklist;
        }
        public static IUser CreateUserDao(IDataBase database)
        {
            Type userType = dalAssembly.GetType(assemblyName + ".UserDAO");
            return Activator.CreateInstance(userType, new object[] { database })
                     as IUser;
        }
        public static IVote CreateVoteDao(IDataBase database)
        {
            Type voteType = dalAssembly.GetType(assemblyName + ".VoteDAO");
            return Activator.CreateInstance(voteType, new object[] { database })
                     as IVote;
        }
    }
}
