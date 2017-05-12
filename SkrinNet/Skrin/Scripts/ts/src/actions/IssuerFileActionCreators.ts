import {REQUEST_FILES,RECEIVE_FILES,REQUEST_UPLOAD_FILE,REQUEST_UPLOAD_INCREASE,
    RECEIVE_UPLOAD_FILES,REQUEST_DELETE_FILE,RECEIVE_DELETE_FILE,REQUEST_FILE_SIZE_LIMIT,RECEIVE_FILE_SIZE_LIMIT} from '../models/Constants'
import {IAction,IActionLoadStatus} from '../models/Common'
import {IUserFile} from '../models/IUserFile'
import IssuerUserFilesAPI from '../api/IssuerUserFilesAPI'
import {ModalActionCreators} from '../actions/ModalActionCreators'

export interface IActionRecieveFileSizeLimit extends IAction{
    file_size_limit:number
}

export interface IActionReceiveUserFiles extends IAction{
    files:IUserFile[]
}


export interface IActionReceiveUserFile extends IAction{
    file:IUserFile
}

export interface IActionReceiveUploadFileStatus extends IActionReceiveUserFile,IActionLoadStatus{

}


export  class IssuerFileActionCreators
{
    private static _requestFiles():IAction{
        return{type:REQUEST_FILES}
    }

    private static _recieveFiles(files:IUserFile[]):IActionReceiveUserFiles{
        return{type:RECEIVE_FILES,files:files}
    }


    static GetFiles(issuer_id:string){
        return async (dispatch:any)=>{
            try
            {
                dispatch(this._requestFiles());
                let files = await IssuerUserFilesAPI.Get(issuer_id);
                dispatch(this._recieveFiles(files));
            }
            catch (error){
                 dispatch(ModalActionCreators.showModal("Ошибка загрузки файлов: " + (error.message || error)));
            }
        }
    }

    private static _requestFileSizeLimit():IAction{
        return {type:REQUEST_FILE_SIZE_LIMIT};
    }

    private static _recieveFileSizeLimit(file_size_limit:number):IActionRecieveFileSizeLimit{
        return{type:RECEIVE_FILE_SIZE_LIMIT,file_size_limit:file_size_limit}
    }

    static GetFileSizeLimit(){
        return async (dispatch:any)=>{
            try
            {
                dispatch(this._requestFileSizeLimit());
                let file_size_limit = await IssuerUserFilesAPI.GetFileSizeLimit();
                dispatch(this._recieveFileSizeLimit(file_size_limit));
            }
            catch (error){
                 dispatch(ModalActionCreators.showModal("Ошибка загрузки файлов: " + (error.message || error)));
            }
        }
    }

    private static _requestUploadFile():IActionLoadStatus{
        return{type:REQUEST_UPLOAD_FILE,load_status:0}
    }

    private static _requestUploadIncrease(notification:any):IActionLoadStatus{
        let status = parseFloat(notification as string);
        return {type:REQUEST_UPLOAD_INCREASE,load_status:status}
    }

    private static _recieveUploadFile(file:IUserFile):IActionReceiveUploadFileStatus{
        return{type:RECEIVE_UPLOAD_FILES,file:file,load_status:null}
    }
/*
    static UploadFile(file:File, issuer_id:string,uploaded_size:number){
         return (dispatch:any)=>{
            dispatch(this._requestUploadFile);
            IssuerUserFilesAPI.Upload(file,issuer_id,uploaded_size
                ,(progress)=>{dispatch(this._requestUploadIncrease(progress));}
                ,(file) => {dispatch(this._recieveUploadFile(file))}
                ,(error) => {dispatch(ModalActionCreators.showModal("Ошибка загрузки файла: " + (error.message || error)));}
            );
         }
}*/

    static UploadFile(file:File, issuer_id:string){
        return async (dispatch:any)=>{
            try{
                dispatch(this._requestUploadFile);
                let res_file = await IssuerUserFilesAPI.Upload(file,issuer_id,(progress)=>{dispatch(this._requestUploadIncrease(progress));})
                dispatch(this._recieveUploadFile(res_file));
            }
            catch(error){
                dispatch(ModalActionCreators.showModal("Ошибка загрузки файла: " + (error.message || error)));
            }
        }
    }

    private static _requestDeleteFile():IAction{
        return{type:REQUEST_DELETE_FILE}
    }

    private static _recieveDeleteFile(file:IUserFile):IActionReceiveUserFile{
        return{type:RECEIVE_DELETE_FILE,file:file}
    }

    static DeleteFile(file_id: string){
        return async (dispatch:any)=>{
            try
            {
                dispatch(this._requestDeleteFile);
                let file = await IssuerUserFilesAPI.Delete(file_id);
                dispatch(this._recieveDeleteFile(file));
            }
             catch (error)
             {
                dispatch(ModalActionCreators.showModal("Ошибка удаления файла: " + (error.message || error)));
             }
        }
    }
}