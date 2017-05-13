export interface IAction{
    type:string
}

export interface IActionId extends IAction{
    id:number | string
}

export interface IActionLoadStatus extends IAction{
    load_status:number | null;
}

export interface IDispatch{
    ():void;
}

export interface IDispatchId{
    (id:number | string):void;
}

export interface IDispatchAny{
    (action:any):void;
}

export interface IDispatchGet{
    (issuer_id:string):void;
}

export interface IDispatchDelete{
    (id:string):void
}

export interface IFunction{ 
    (payload:any | any[]):void;
}