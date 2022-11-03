using Core.Entities;
using DataAccess.TypeConfigurations;
using Microsoft.EntityFrameworkCore;

namespace DataAccess;

public class EntityFrameworkUnitOfWork : DbContext, IUnitOfWork
{
    public EntityFrameworkUnitOfWork(DbContextOptions<EntityFrameworkUnitOfWork> options) : base(options) { }
    
    public IQueryable<T> Collection<T>() where T : class
    {
        return Set<T>();
    }

    public T Create<T>(T entity) where T : class
    {
        return Set<T>().Add(entity).Entity;
    }

    public void Delete<T>(T entity) where T : class
    {
        Set<T>().Remove(entity);
    }

    public async Task SaveAsync(CancellationToken cancellationToken)
    {
        await SaveChangesAsync(cancellationToken);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        new AuthRoleConfiguration().Configure(modelBuilder.Entity<AuthRole>());
        new DoctorConfiguration().Configure(modelBuilder.Entity<Doctor>());
        new ManagerConfiguration().Configure(modelBuilder.Entity<Manager>());
        new UserConfiguration().Configure(modelBuilder.Entity<User>());
        new PatientConfiguration().Configure(modelBuilder.Entity<Patient>());
    }
}