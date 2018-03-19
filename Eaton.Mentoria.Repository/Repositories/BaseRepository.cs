using System;
using System.Collections.Generic;
using Eaton.Mentoria.Domain.Contracts;
using System.Linq;
using Eaton.Mentoria.Repository.Context;
using Microsoft.EntityFrameworkCore;

namespace Eaton.Mentoria.Repository.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
       
        private readonly IMentoriaContext _dbContext;

        public BaseRepository(IMentoriaContext imentoriacontext)
        {
            _dbContext = imentoriacontext;
        }

        public int Atualizar(T dados)
        {
            
            try
            {
                _dbContext.Set<T>().Update(dados);
                return _dbContext.SaveChanges();
            }
            catch (System.Exception ex)
            {
                
                throw new System.Exception(ex.Message);
            }
        }            

        public T BuscarPorId(int id, string[] includes = null)
        {
            try
            {
                var chavePrimaria = _dbContext.Model.FindEntityType(typeof(T)).FindPrimaryKey().Properties[0];

                var query = _dbContext.Set<T>().AsQueryable();

                if(includes == null) return query.FirstOrDefault(e => EF.Property<int>(e, chavePrimaria.Name) == id);

                    foreach (var item in includes)
                    {
                        query = query.Include(item);
                    } 

                return _dbContext.Set<T>().FirstOrDefault(e => EF.Property<int>(e, chavePrimaria.Name) == id);
            }
            catch (System.Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public int Deletar(T dados)
        {
            
            try
            {
                _dbContext.Set<T>().Remove(dados);
                return _dbContext.SaveChanges();
            }
            catch (System.Exception ex)
            {
                
                throw new Exception(ex.Message);
            }
             
        }

      

        public int Inserir(T dados)
        {
           try
            {
                _dbContext.Set<T>().Add(dados);
                return _dbContext.SaveChanges();
            }
            catch (System.Exception ex)
            {
                
                throw new Exception(ex.Message);
            }
        }

        public IEnumerable<T> Listar(string[] includes = null)
        {
            
            try
            {
                var query = _dbContext.Set<T>().AsQueryable();

                if(includes == null) return query.ToList();

                foreach (var item in includes)
                {
                    query = query.Include(item);
                }  

                return query.ToList();              
            }
            catch (System.Exception ex)
            {
                
                throw new Exception(ex.Message);
            }
             
        }

        
    }  
     
}