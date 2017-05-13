import {IModalState} from './IModalState'
import {ICanUseCloudModalState} from './ICanUseCloudModalState'
import {IUserFile} from './IUserFile'
import {IUserNote} from './IUserNote'
import {IAddress} from './IAddress'


export interface IIssuerState{
    files:IUserFile[],
    upload_progress:number | null,
    modals:IModalState,
    notes:IUserNote[],
    new_notes:IUserNote[],
    addresses:IAddress[],
    issuer_id:string,
    file_size_limit:number,
    can_use_cloud:boolean,
    can_use_cloud_modals:ICanUseCloudModalState
}