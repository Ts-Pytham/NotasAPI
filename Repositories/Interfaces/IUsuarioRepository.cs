﻿namespace NotasAPI.Repositories.Interfaces;

public interface IUsuarioRepository : IRepository<Usuario>
{
    Task<UsuarioDTO> CreateUsuarioAsync(UsuarioInsertDTO usuarioDTO);

    Task<UsuarioDTO> LoginUsuarioAsync(UsuarioLoginDTO usuarioDTO);

    public Task<bool> CheckMonitor(long idMonitor);
}
