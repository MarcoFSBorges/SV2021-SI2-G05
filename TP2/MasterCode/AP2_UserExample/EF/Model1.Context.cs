﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace EF
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class Jogos_entities : DbContext
    {
        public Jogos_entities()
            : base("name=Jogos_entities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<CLAN> CLAN { get; set; }
        public virtual DbSet<ITEM> ITEM { get; set; }
        public virtual DbSet<LOGIN> LOGIN { get; set; }
        public virtual DbSet<MATCH> MATCH { get; set; }
        public virtual DbSet<NONREGISTEREDPLAYER> NONREGISTEREDPLAYER { get; set; }
        public virtual DbSet<PLAYER> PLAYER { get; set; }
        public virtual DbSet<REGISTEREDPLAYER> REGISTEREDPLAYER { get; set; }
        public virtual DbSet<STATE> STATE { get; set; }
        public virtual DbSet<TYPE> TYPE { get; set; }
        public virtual DbSet<GLOBAL_CONFIGURATION> GLOBAL_CONFIGURATION { get; set; }
        public virtual DbSet<players_view> players_view { get; set; }
    
        [DbFunction("Jogos_entities", "f_getClanInfo")]
        public virtual IQueryable<f_getClanInfo_Result> f_getClanInfo(string clanName)
        {
            var clanNameParameter = clanName != null ?
                new ObjectParameter("clanName", clanName) :
                new ObjectParameter("clanName", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.CreateQuery<f_getClanInfo_Result>("[Jogos_entities].[f_getClanInfo](@clanName)", clanNameParameter);
        }
    
        [DbFunction("Jogos_entities", "f_getPlayerGamesFromYear")]
        public virtual IQueryable<f_getPlayerGamesFromYear_Result> f_getPlayerGamesFromYear(Nullable<int> player_id, Nullable<int> year)
        {
            var player_idParameter = player_id.HasValue ?
                new ObjectParameter("player_id", player_id) :
                new ObjectParameter("player_id", typeof(int));
    
            var yearParameter = year.HasValue ?
                new ObjectParameter("year", year) :
                new ObjectParameter("year", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.CreateQuery<f_getPlayerGamesFromYear_Result>("[Jogos_entities].[f_getPlayerGamesFromYear](@player_id, @year)", player_idParameter, yearParameter);
        }
    
        public virtual int createPlayerWithOptions(Nullable<int> player_id, Nullable<int> login_id, Nullable<int> life_points, Nullable<int> strength_points, Nullable<int> speed_points, Nullable<int> item_id, Nullable<int> clan_id)
        {
            var player_idParameter = player_id.HasValue ?
                new ObjectParameter("player_id", player_id) :
                new ObjectParameter("player_id", typeof(int));
    
            var login_idParameter = login_id.HasValue ?
                new ObjectParameter("login_id", login_id) :
                new ObjectParameter("login_id", typeof(int));
    
            var life_pointsParameter = life_points.HasValue ?
                new ObjectParameter("life_points", life_points) :
                new ObjectParameter("life_points", typeof(int));
    
            var strength_pointsParameter = strength_points.HasValue ?
                new ObjectParameter("strength_points", strength_points) :
                new ObjectParameter("strength_points", typeof(int));
    
            var speed_pointsParameter = speed_points.HasValue ?
                new ObjectParameter("speed_points", speed_points) :
                new ObjectParameter("speed_points", typeof(int));
    
            var item_idParameter = item_id.HasValue ?
                new ObjectParameter("item_id", item_id) :
                new ObjectParameter("item_id", typeof(int));
    
            var clan_idParameter = clan_id.HasValue ?
                new ObjectParameter("clan_id", clan_id) :
                new ObjectParameter("clan_id", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("createPlayerWithOptions", player_idParameter, login_idParameter, life_pointsParameter, strength_pointsParameter, speed_pointsParameter, item_idParameter, clan_idParameter);
        }
    
        public virtual int p_createMatch(Nullable<int> player_one, Nullable<int> player_two, string description)
        {
            var player_oneParameter = player_one.HasValue ?
                new ObjectParameter("player_one", player_one) :
                new ObjectParameter("player_one", typeof(int));
    
            var player_twoParameter = player_two.HasValue ?
                new ObjectParameter("player_two", player_two) :
                new ObjectParameter("player_two", typeof(int));
    
            var descriptionParameter = description != null ?
                new ObjectParameter("description", description) :
                new ObjectParameter("description", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("p_createMatch", player_oneParameter, player_twoParameter, descriptionParameter);
        }
    
        public virtual int p_giveItemToPlayer(Nullable<int> player_id, Nullable<int> item_id)
        {
            var player_idParameter = player_id.HasValue ?
                new ObjectParameter("player_id", player_id) :
                new ObjectParameter("player_id", typeof(int));
    
            var item_idParameter = item_id.HasValue ?
                new ObjectParameter("item_id", item_id) :
                new ObjectParameter("item_id", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("p_giveItemToPlayer", player_idParameter, item_idParameter);
        }
    }
}
