﻿using NotasAPI.Entities;

namespace NotasAPI.Core.Interfaces;

public interface IRecordatorioBusiness
{
    Task<Response<RecordatorioDTO>> CreateRecordatorio(long idUsuario, RecordatorioInsertDTO insertDTO);

    Task<Response<IEnumerable<RecordatorioDTO>>> GetAllRecordatorios(long idUsuario);

    Task<Response<RecordatorioWithGroupDTO>> CreateRecordatorioInGroup(long idUsuario, long idGrupo, RecordatorioInsertDTO insertDTO);

    Task<Response<RecordatorioUpdateDTO>> UpdateRecordatorio(RecordatorioUpdateDTO updateDTO);

    Task<Response<RecordatorioDeleteDTO>> DeleteRecordatorio(long idRecordatorio);
}
