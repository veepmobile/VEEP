import {RECEIVE_FILES,RECEIVE_UPLOAD_FILES,RECEIVE_DELETE_FILE,REQUEST_UPLOAD_FILE,
    REQUEST_UPLOAD_INCREASE,RECEIVE_FILE_SIZE_LIMIT} from '../models/Constants'
import {IUserFile} from '../models/IUserFile'
import {IActionLoadStatus,IAction} from '../models/Common'
import {IActionReceiveUserFiles,IActionReceiveUserFile,IActionRecieveFileSizeLimit} from '../actions/IssuerFileActionCreators'
import {DeepCopy} from '../lib/Utilites'


export const user_files=(state:IUserFile[]=[],action:IAction)=>{
    let file_index:number;
    let new_file:IUserFile;

    switch(action.type){
        case RECEIVE_FILES:
            return DeepCopy<IActionReceiveUserFiles>(action).files;
        case RECEIVE_UPLOAD_FILES:
            state.push(DeepCopy<IActionReceiveUserFile>(action).file);
            return[...state];
        case RECEIVE_DELETE_FILE:
            new_file=DeepCopy<IActionReceiveUserFile>(action).file;
            file_index=state.indexOf(state.filter(p=>p.id===new_file.id)[0]);
            state.splice(file_index,1);
            return[...state];
        default:
            return state;
    }
}

export const upload_progress=(state:number|null = null,action: IActionLoadStatus)=>{
    switch(action.type){
        case REQUEST_UPLOAD_FILE:
        case REQUEST_UPLOAD_INCREASE:
        case RECEIVE_UPLOAD_FILES:
            return action.load_status;
        default:
            return state;
    }
}

export const file_size_limit=(state:number=0,action:IActionRecieveFileSizeLimit)=>{
    switch(action.type){
        case RECEIVE_FILE_SIZE_LIMIT:
            return action.file_size_limit;
        default:
            return state;
    }
}