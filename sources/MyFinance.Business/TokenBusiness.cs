using MyFinance.Core;
using MyFinance.Data.Infrastructure;
using MyFinance.Domain;
using MyFinance.Domain.BusinessModel;
using MyFinance.Domain.Entities;
using MyFinance.Extention;
using MyFinance.Utils;
using System;
using System.Linq;

namespace MyFinance.Business
{
    public interface ITokenBusiness {
        TokenModel GenerateToken(int userId);
        bool ValidateToken(string tokenId);
        bool Kill(string tokenId);
        bool DeleteByUserId(int userId);
        string CheckExistStoreToken(string storeName);
        bool AddStoreToken(StoreTokenModel model);
    }
    public class TokenBusiness :BusinessBase, ITokenBusiness
    {
          #region Private member variables.
        private readonly IUnitOfWork unitOfWork;
         #endregion

        #region Public constructor.
        /// <summary>
        /// Public constructor.
        /// </summary>
        public TokenBusiness(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        #endregion

        #region Public member methods.

        /// <summary>
        ///  Function to generate unique token with expiry against the provided userId.
        ///  Also add a record in database for generated token.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public TokenModel GenerateToken(int userId)
        {
            string token = Guid.NewGuid().ToString();
            DateTime issuedOn = DateTime.Now;
            DateTime expiredOn = DateTime.Now.AddDays(7);
            var repo= unitOfWork.Repository<Token>();
            var tokenCurrent = repo.Get(a => a.UserId == userId);
            var user = unitOfWork.Repository<User>().GetById(userId);
            if(tokenCurrent!=null)
            {
                tokenCurrent.ExpiresOn = tokenCurrent.ExpiresOn.AddDays(7);
                token = tokenCurrent.AuthToken;
            }
            else
            {
                var tokendomain = new Token
                {
                    UserId = userId,
                    HotelId=user.HotelId,
                    AuthToken = token,
                    IssuedOn = issuedOn,
                    ExpiresOn = expiredOn
                };
                repo.Add(tokendomain);
            }
            var tokenModel = new TokenModel()
            {
                UserId = userId,
                HotelId = user.HotelId,
                IssuedOn = issuedOn,
                ExpiresOn = expiredOn,
                AuthToken = token
            };

            return tokenModel;
        }

        /// <summary>
        /// Method to validate token against expiry and existence in database.
        /// </summary>
        /// <param name="tokenId"></param>
        /// <returns></returns>
        public bool ValidateToken(string tokenId)
        {
            var m_tokenRepository = unitOfWork.Repository<Token>();

            var token = m_tokenRepository.Get(t => t.AuthToken == tokenId);
            if (token != null && !(DateTime.Now > token.ExpiresOn))
            {
                var user = unitOfWork.Repository<User>().GetById(token.UserId);
               
                var context = new UserContext()
                {
                    HotelId=token.HotelId,
                    UserId = token.UserId,
                  
                    UserName=user.Email,
                    FullName=user.FullName,
                    TokenId = token.AuthToken

                };

                if (user.IsOwner)
                {
                    var m_ownerHotel = unitOfWork.Repository<OwnerHotel>().Get(a => a.UserOwnerId == user.Id && a.IsSelected);
                    context.HotelId = m_ownerHotel.HotelId;
                }
                WorkContext.BizKasaContext = context;
                
                unitOfWork.Commit();
                return true;
            }
            return false;
        }

        /// <summary>
        /// Method to kill the provided token id.
        /// </summary>
        /// <param name="tokenId">true for successful delete</param>
        public bool Kill(string tokenId)
        {
            unitOfWork.Repository<Token>().Delete(x => x.AuthToken == tokenId);
            unitOfWork.Commit();
            var isNotDeleted = unitOfWork.Repository<Token>().GetMany(x => x.AuthToken == tokenId).Any();
            if (isNotDeleted) { return false; }
            return true;
        }

        /// <summary>
        /// Delete tokens for the specific deleted user
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>true for successful delete</returns>
        public bool DeleteByUserId(int userId)
        {
            unitOfWork.Repository<Token>().Delete(x => x.UserId == userId);
            unitOfWork.Commit();

            var isNotDeleted = unitOfWork.Repository<Token>().GetMany(x => x.UserId == userId).Any();
            return !isNotDeleted;
        }

        #endregion

        #region StoreToken
        public string CheckExistStoreToken(string storeName)
        {
            var shop = unitOfWork.Repository<Token>().GetMany(a => a.StoreName == storeName).FirstOrDefault();
            return shop == null ? string.Empty : shop.AuthToken;
        }


        public bool AddStoreToken(StoreTokenModel model)
        {
            var repo = unitOfWork.Repository<Token>();
            var store = repo.Get(a => a.StoreName == model.StoreName);
            if (store != null)
            {
                store.AuthToken = model.access_token;
                store.IssuedOn = DateTime.Now;
                store.ExpiresOn = DateTime.Now;
                repo.Update(store);
            }else
            {
                var tokendomain = new Token
                {
                    AuthToken = model.access_token,
                    StoreName = model.StoreName,
                    ExpiresOn = DateTime.Now,
                    IssuedOn = DateTime.Now
                };
                repo.Add(tokendomain);
            }
            
            unitOfWork.Commit();
            return !this.HasError;
        }
        #endregion
    }
}
