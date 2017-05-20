import {IActionReceive,IAction,IActionLoadStatus} from './Interfaces'
import {IModalState} from './DialogInterfaces'
import UserFile from './UserFile'


export interface IActionReceiveUserFiles extends IActionReceive{
    files:UserFile[]
}


export interface IActionReceiveUploadFileStatus extends IActionReceiveUserFiles,IActionLoadStatus{

}

export interface IActionUserFiles extends IAction,IActionReceive{
    files:UserFile[]
}

export interface IIssuerState{
    files:UserFile[],
    upload_progress:number | null,
    modals:IModalState
}

export interface IDispatchGetFiles{
    (user_id:number,issuer_id:string):void;
}

export interface IDispatchUploadFile{
    (file:File, user_id: number, issuer_id:string):void;
}

export interface IDipatchDeleteFile{
    (file_id:string, file_name:string):void
}