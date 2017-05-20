export  const logger=(store:any)=>(next:any)=>(action:any)=>{ 
    if(typeof action!="function"){
        console.log('dispatching: ',action);
    }
    return next(action);
}