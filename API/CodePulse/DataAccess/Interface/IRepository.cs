﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Interface
{
    public interface IRepository<T>
    {
        //CRUD - create, read, update, delete
        List<T> GetAll(); //read

        T GetById(Guid id); //read

        void Add(T entity); //create

        void Update(T entity); //update

        void Delete(T entity); //delete
    }
}
