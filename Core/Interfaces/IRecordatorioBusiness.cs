namespace NotasAPI.Core.Interfaces;

public interface IRecordatorioBusiness
{
    Task<Response<RecordatorioInsertDTO>> CreateRecordatorio(int idUsuario, RecordatorioInsertDTO insertDTO);

    Task<Response<IEnumerable<RecordatorioDTO>>> GetAllRecordatorios(int idUsuario);

    Task<Response<RecordatorioWithGroupDTO>> CreateRecordatorioInGroup(int idUsuario, int idGrupo, RecordatorioInsertDTO insertDTO);
}
