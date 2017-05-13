/******/ (function(modules) { // webpackBootstrap
/******/ 	// The module cache
/******/ 	var installedModules = {};
/******/
/******/ 	// The require function
/******/ 	function __webpack_require__(moduleId) {
/******/
/******/ 		// Check if module is in cache
/******/ 		if(installedModules[moduleId])
/******/ 			return installedModules[moduleId].exports;
/******/
/******/ 		// Create a new module (and put it into the cache)
/******/ 		var module = installedModules[moduleId] = {
/******/ 			exports: {},
/******/ 			id: moduleId,
/******/ 			loaded: false
/******/ 		};
/******/
/******/ 		// Execute the module function
/******/ 		modules[moduleId].call(module.exports, module, module.exports, __webpack_require__);
/******/
/******/ 		// Flag the module as loaded
/******/ 		module.loaded = true;
/******/
/******/ 		// Return the exports of the module
/******/ 		return module.exports;
/******/ 	}
/******/
/******/
/******/ 	// expose the modules object (__webpack_modules__)
/******/ 	__webpack_require__.m = modules;
/******/
/******/ 	// expose the module cache
/******/ 	__webpack_require__.c = installedModules;
/******/
/******/ 	// __webpack_public_path__
/******/ 	__webpack_require__.p = "";
/******/
/******/ 	// Load entry module and return exports
/******/ 	return __webpack_require__(0);
/******/ })
/************************************************************************/
/******/ ([
/* 0 */
/***/ function(module, exports, __webpack_require__) {

	"use strict";
	var React = __webpack_require__(1);
	var ReactDOM = __webpack_require__(2);
	var react_redux_1 = __webpack_require__(3);
	var redux_1 = __webpack_require__(11);
	var redux_thunk_1 = __webpack_require__(44);
	var rootReducer_1 = __webpack_require__(45);
	var IssuerUserFilesComponent_1 = __webpack_require__(54);
	var IssuerUserNotesComponent_1 = __webpack_require__(62);
	var IssuerUserAddressComponent_1 = __webpack_require__(67);
	var ConfirmModalComponent_1 = __webpack_require__(73);
	var CanUseCloudCheckComponent_1 = __webpack_require__(75);
	var issuer_id = document.getElementById("cloud_container").getAttribute("data-id");
	var InitState = {
	    issuer_id: issuer_id
	};
	var IssuerStore = redux_1.createStore(rootReducer_1.default, InitState, redux_1.applyMiddleware(/*logger,*/ redux_thunk_1.default));
	ReactDOM.render(React.createElement(react_redux_1.Provider, { store: IssuerStore },
	    React.createElement("div", null,
	        React.createElement(CanUseCloudCheckComponent_1.default, null),
	        React.createElement(ConfirmModalComponent_1.default, null),
	        React.createElement(IssuerUserAddressComponent_1.default, null),
	        React.createElement(IssuerUserNotesComponent_1.default, null),
	        React.createElement(IssuerUserFilesComponent_1.default, null))), document.getElementById("cloud_container"));


/***/ },
/* 1 */
/***/ function(module, exports) {

	module.exports = React;

/***/ },
/* 2 */
/***/ function(module, exports) {

	module.exports = ReactDOM;

/***/ },
/* 3 */
/***/ function(module, exports, __webpack_require__) {

	'use strict';
	
	exports.__esModule = true;
	exports.connect = exports.Provider = undefined;
	
	var _Provider = __webpack_require__(4);
	
	var _Provider2 = _interopRequireDefault(_Provider);
	
	var _connect = __webpack_require__(8);
	
	var _connect2 = _interopRequireDefault(_connect);
	
	function _interopRequireDefault(obj) { return obj && obj.__esModule ? obj : { "default": obj }; }
	
	exports.Provider = _Provider2["default"];
	exports.connect = _connect2["default"];

/***/ },
/* 4 */
/***/ function(module, exports, __webpack_require__) {

	/* WEBPACK VAR INJECTION */(function(process) {'use strict';
	
	exports.__esModule = true;
	exports["default"] = undefined;
	
	var _react = __webpack_require__(1);
	
	var _storeShape = __webpack_require__(6);
	
	var _storeShape2 = _interopRequireDefault(_storeShape);
	
	var _warning = __webpack_require__(7);
	
	var _warning2 = _interopRequireDefault(_warning);
	
	function _interopRequireDefault(obj) { return obj && obj.__esModule ? obj : { "default": obj }; }
	
	function _classCallCheck(instance, Constructor) { if (!(instance instanceof Constructor)) { throw new TypeError("Cannot call a class as a function"); } }
	
	function _possibleConstructorReturn(self, call) { if (!self) { throw new ReferenceError("this hasn't been initialised - super() hasn't been called"); } return call && (typeof call === "object" || typeof call === "function") ? call : self; }
	
	function _inherits(subClass, superClass) { if (typeof superClass !== "function" && superClass !== null) { throw new TypeError("Super expression must either be null or a function, not " + typeof superClass); } subClass.prototype = Object.create(superClass && superClass.prototype, { constructor: { value: subClass, enumerable: false, writable: true, configurable: true } }); if (superClass) Object.setPrototypeOf ? Object.setPrototypeOf(subClass, superClass) : subClass.__proto__ = superClass; }
	
	var didWarnAboutReceivingStore = false;
	function warnAboutReceivingStore() {
	  if (didWarnAboutReceivingStore) {
	    return;
	  }
	  didWarnAboutReceivingStore = true;
	
	  (0, _warning2["default"])('<Provider> does not support changing `store` on the fly. ' + 'It is most likely that you see this error because you updated to ' + 'Redux 2.x and React Redux 2.x which no longer hot reload reducers ' + 'automatically. See https://github.com/reactjs/react-redux/releases/' + 'tag/v2.0.0 for the migration instructions.');
	}
	
	var Provider = function (_Component) {
	  _inherits(Provider, _Component);
	
	  Provider.prototype.getChildContext = function getChildContext() {
	    return { store: this.store };
	  };
	
	  function Provider(props, context) {
	    _classCallCheck(this, Provider);
	
	    var _this = _possibleConstructorReturn(this, _Component.call(this, props, context));
	
	    _this.store = props.store;
	    return _this;
	  }
	
	  Provider.prototype.render = function render() {
	    return _react.Children.only(this.props.children);
	  };
	
	  return Provider;
	}(_react.Component);
	
	exports["default"] = Provider;
	
	
	if (process.env.NODE_ENV !== 'production') {
	  Provider.prototype.componentWillReceiveProps = function (nextProps) {
	    var store = this.store;
	    var nextStore = nextProps.store;
	
	
	    if (store !== nextStore) {
	      warnAboutReceivingStore();
	    }
	  };
	}
	
	Provider.propTypes = {
	  store: _storeShape2["default"].isRequired,
	  children: _react.PropTypes.element.isRequired
	};
	Provider.childContextTypes = {
	  store: _storeShape2["default"].isRequired
	};
	/* WEBPACK VAR INJECTION */}.call(exports, __webpack_require__(5)))

/***/ },
/* 5 */
/***/ function(module, exports) {

	// shim for using process in browser
	var process = module.exports = {};
	
	// cached from whatever global is present so that test runners that stub it
	// don't break things.  But we need to wrap it in a try catch in case it is
	// wrapped in strict mode code which doesn't define any globals.  It's inside a
	// function because try/catches deoptimize in certain engines.
	
	var cachedSetTimeout;
	var cachedClearTimeout;
	
	function defaultSetTimout() {
	    throw new Error('setTimeout has not been defined');
	}
	function defaultClearTimeout () {
	    throw new Error('clearTimeout has not been defined');
	}
	(function () {
	    try {
	        if (typeof setTimeout === 'function') {
	            cachedSetTimeout = setTimeout;
	        } else {
	            cachedSetTimeout = defaultSetTimout;
	        }
	    } catch (e) {
	        cachedSetTimeout = defaultSetTimout;
	    }
	    try {
	        if (typeof clearTimeout === 'function') {
	            cachedClearTimeout = clearTimeout;
	        } else {
	            cachedClearTimeout = defaultClearTimeout;
	        }
	    } catch (e) {
	        cachedClearTimeout = defaultClearTimeout;
	    }
	} ())
	function runTimeout(fun) {
	    if (cachedSetTimeout === setTimeout) {
	        //normal enviroments in sane situations
	        return setTimeout(fun, 0);
	    }
	    // if setTimeout wasn't available but was latter defined
	    if ((cachedSetTimeout === defaultSetTimout || !cachedSetTimeout) && setTimeout) {
	        cachedSetTimeout = setTimeout;
	        return setTimeout(fun, 0);
	    }
	    try {
	        // when when somebody has screwed with setTimeout but no I.E. maddness
	        return cachedSetTimeout(fun, 0);
	    } catch(e){
	        try {
	            // When we are in I.E. but the script has been evaled so I.E. doesn't trust the global object when called normally
	            return cachedSetTimeout.call(null, fun, 0);
	        } catch(e){
	            // same as above but when it's a version of I.E. that must have the global object for 'this', hopfully our context correct otherwise it will throw a global error
	            return cachedSetTimeout.call(this, fun, 0);
	        }
	    }
	
	
	}
	function runClearTimeout(marker) {
	    if (cachedClearTimeout === clearTimeout) {
	        //normal enviroments in sane situations
	        return clearTimeout(marker);
	    }
	    // if clearTimeout wasn't available but was latter defined
	    if ((cachedClearTimeout === defaultClearTimeout || !cachedClearTimeout) && clearTimeout) {
	        cachedClearTimeout = clearTimeout;
	        return clearTimeout(marker);
	    }
	    try {
	        // when when somebody has screwed with setTimeout but no I.E. maddness
	        return cachedClearTimeout(marker);
	    } catch (e){
	        try {
	            // When we are in I.E. but the script has been evaled so I.E. doesn't  trust the global object when called normally
	            return cachedClearTimeout.call(null, marker);
	        } catch (e){
	            // same as above but when it's a version of I.E. that must have the global object for 'this', hopfully our context correct otherwise it will throw a global error.
	            // Some versions of I.E. have different rules for clearTimeout vs setTimeout
	            return cachedClearTimeout.call(this, marker);
	        }
	    }
	
	
	
	}
	var queue = [];
	var draining = false;
	var currentQueue;
	var queueIndex = -1;
	
	function cleanUpNextTick() {
	    if (!draining || !currentQueue) {
	        return;
	    }
	    draining = false;
	    if (currentQueue.length) {
	        queue = currentQueue.concat(queue);
	    } else {
	        queueIndex = -1;
	    }
	    if (queue.length) {
	        drainQueue();
	    }
	}
	
	function drainQueue() {
	    if (draining) {
	        return;
	    }
	    var timeout = runTimeout(cleanUpNextTick);
	    draining = true;
	
	    var len = queue.length;
	    while(len) {
	        currentQueue = queue;
	        queue = [];
	        while (++queueIndex < len) {
	            if (currentQueue) {
	                currentQueue[queueIndex].run();
	            }
	        }
	        queueIndex = -1;
	        len = queue.length;
	    }
	    currentQueue = null;
	    draining = false;
	    runClearTimeout(timeout);
	}
	
	process.nextTick = function (fun) {
	    var args = new Array(arguments.length - 1);
	    if (arguments.length > 1) {
	        for (var i = 1; i < arguments.length; i++) {
	            args[i - 1] = arguments[i];
	        }
	    }
	    queue.push(new Item(fun, args));
	    if (queue.length === 1 && !draining) {
	        runTimeout(drainQueue);
	    }
	};
	
	// v8 likes predictible objects
	function Item(fun, array) {
	    this.fun = fun;
	    this.array = array;
	}
	Item.prototype.run = function () {
	    this.fun.apply(null, this.array);
	};
	process.title = 'browser';
	process.browser = true;
	process.env = {};
	process.argv = [];
	process.version = ''; // empty string to avoid regexp issues
	process.versions = {};
	
	function noop() {}
	
	process.on = noop;
	process.addListener = noop;
	process.once = noop;
	process.off = noop;
	process.removeListener = noop;
	process.removeAllListeners = noop;
	process.emit = noop;
	
	process.binding = function (name) {
	    throw new Error('process.binding is not supported');
	};
	
	process.cwd = function () { return '/' };
	process.chdir = function (dir) {
	    throw new Error('process.chdir is not supported');
	};
	process.umask = function() { return 0; };


/***/ },
/* 6 */
/***/ function(module, exports, __webpack_require__) {

	'use strict';
	
	exports.__esModule = true;
	
	var _react = __webpack_require__(1);
	
	exports["default"] = _react.PropTypes.shape({
	  subscribe: _react.PropTypes.func.isRequired,
	  dispatch: _react.PropTypes.func.isRequired,
	  getState: _react.PropTypes.func.isRequired
	});

/***/ },
/* 7 */
/***/ function(module, exports) {

	'use strict';
	
	exports.__esModule = true;
	exports["default"] = warning;
	/**
	 * Prints a warning in the console if it exists.
	 *
	 * @param {String} message The warning message.
	 * @returns {void}
	 */
	function warning(message) {
	  /* eslint-disable no-console */
	  if (typeof console !== 'undefined' && typeof console.error === 'function') {
	    console.error(message);
	  }
	  /* eslint-enable no-console */
	  try {
	    // This error was thrown as a convenience so that if you enable
	    // "break on all exceptions" in your console,
	    // it would pause the execution at this line.
	    throw new Error(message);
	    /* eslint-disable no-empty */
	  } catch (e) {}
	  /* eslint-enable no-empty */
	}

/***/ },
/* 8 */
/***/ function(module, exports, __webpack_require__) {

	/* WEBPACK VAR INJECTION */(function(process) {'use strict';
	
	exports.__esModule = true;
	
	var _extends = Object.assign || function (target) { for (var i = 1; i < arguments.length; i++) { var source = arguments[i]; for (var key in source) { if (Object.prototype.hasOwnProperty.call(source, key)) { target[key] = source[key]; } } } return target; };
	
	exports["default"] = connect;
	
	var _react = __webpack_require__(1);
	
	var _storeShape = __webpack_require__(6);
	
	var _storeShape2 = _interopRequireDefault(_storeShape);
	
	var _shallowEqual = __webpack_require__(9);
	
	var _shallowEqual2 = _interopRequireDefault(_shallowEqual);
	
	var _wrapActionCreators = __webpack_require__(10);
	
	var _wrapActionCreators2 = _interopRequireDefault(_wrapActionCreators);
	
	var _warning = __webpack_require__(7);
	
	var _warning2 = _interopRequireDefault(_warning);
	
	var _isPlainObject = __webpack_require__(32);
	
	var _isPlainObject2 = _interopRequireDefault(_isPlainObject);
	
	var _hoistNonReactStatics = __webpack_require__(42);
	
	var _hoistNonReactStatics2 = _interopRequireDefault(_hoistNonReactStatics);
	
	var _invariant = __webpack_require__(43);
	
	var _invariant2 = _interopRequireDefault(_invariant);
	
	function _interopRequireDefault(obj) { return obj && obj.__esModule ? obj : { "default": obj }; }
	
	function _classCallCheck(instance, Constructor) { if (!(instance instanceof Constructor)) { throw new TypeError("Cannot call a class as a function"); } }
	
	function _possibleConstructorReturn(self, call) { if (!self) { throw new ReferenceError("this hasn't been initialised - super() hasn't been called"); } return call && (typeof call === "object" || typeof call === "function") ? call : self; }
	
	function _inherits(subClass, superClass) { if (typeof superClass !== "function" && superClass !== null) { throw new TypeError("Super expression must either be null or a function, not " + typeof superClass); } subClass.prototype = Object.create(superClass && superClass.prototype, { constructor: { value: subClass, enumerable: false, writable: true, configurable: true } }); if (superClass) Object.setPrototypeOf ? Object.setPrototypeOf(subClass, superClass) : subClass.__proto__ = superClass; }
	
	var defaultMapStateToProps = function defaultMapStateToProps(state) {
	  return {};
	}; // eslint-disable-line no-unused-vars
	var defaultMapDispatchToProps = function defaultMapDispatchToProps(dispatch) {
	  return { dispatch: dispatch };
	};
	var defaultMergeProps = function defaultMergeProps(stateProps, dispatchProps, parentProps) {
	  return _extends({}, parentProps, stateProps, dispatchProps);
	};
	
	function getDisplayName(WrappedComponent) {
	  return WrappedComponent.displayName || WrappedComponent.name || 'Component';
	}
	
	var errorObject = { value: null };
	function tryCatch(fn, ctx) {
	  try {
	    return fn.apply(ctx);
	  } catch (e) {
	    errorObject.value = e;
	    return errorObject;
	  }
	}
	
	// Helps track hot reloading.
	var nextVersion = 0;
	
	function connect(mapStateToProps, mapDispatchToProps, mergeProps) {
	  var options = arguments.length > 3 && arguments[3] !== undefined ? arguments[3] : {};
	
	  var shouldSubscribe = Boolean(mapStateToProps);
	  var mapState = mapStateToProps || defaultMapStateToProps;
	
	  var mapDispatch = void 0;
	  if (typeof mapDispatchToProps === 'function') {
	    mapDispatch = mapDispatchToProps;
	  } else if (!mapDispatchToProps) {
	    mapDispatch = defaultMapDispatchToProps;
	  } else {
	    mapDispatch = (0, _wrapActionCreators2["default"])(mapDispatchToProps);
	  }
	
	  var finalMergeProps = mergeProps || defaultMergeProps;
	  var _options$pure = options.pure,
	      pure = _options$pure === undefined ? true : _options$pure,
	      _options$withRef = options.withRef,
	      withRef = _options$withRef === undefined ? false : _options$withRef;
	
	  var checkMergedEquals = pure && finalMergeProps !== defaultMergeProps;
	
	  // Helps track hot reloading.
	  var version = nextVersion++;
	
	  return function wrapWithConnect(WrappedComponent) {
	    var connectDisplayName = 'Connect(' + getDisplayName(WrappedComponent) + ')';
	
	    function checkStateShape(props, methodName) {
	      if (!(0, _isPlainObject2["default"])(props)) {
	        (0, _warning2["default"])(methodName + '() in ' + connectDisplayName + ' must return a plain object. ' + ('Instead received ' + props + '.'));
	      }
	    }
	
	    function computeMergedProps(stateProps, dispatchProps, parentProps) {
	      var mergedProps = finalMergeProps(stateProps, dispatchProps, parentProps);
	      if (process.env.NODE_ENV !== 'production') {
	        checkStateShape(mergedProps, 'mergeProps');
	      }
	      return mergedProps;
	    }
	
	    var Connect = function (_Component) {
	      _inherits(Connect, _Component);
	
	      Connect.prototype.shouldComponentUpdate = function shouldComponentUpdate() {
	        return !pure || this.haveOwnPropsChanged || this.hasStoreStateChanged;
	      };
	
	      function Connect(props, context) {
	        _classCallCheck(this, Connect);
	
	        var _this = _possibleConstructorReturn(this, _Component.call(this, props, context));
	
	        _this.version = version;
	        _this.store = props.store || context.store;
	
	        (0, _invariant2["default"])(_this.store, 'Could not find "store" in either the context or ' + ('props of "' + connectDisplayName + '". ') + 'Either wrap the root component in a <Provider>, ' + ('or explicitly pass "store" as a prop to "' + connectDisplayName + '".'));
	
	        var storeState = _this.store.getState();
	        _this.state = { storeState: storeState };
	        _this.clearCache();
	        return _this;
	      }
	
	      Connect.prototype.computeStateProps = function computeStateProps(store, props) {
	        if (!this.finalMapStateToProps) {
	          return this.configureFinalMapState(store, props);
	        }
	
	        var state = store.getState();
	        var stateProps = this.doStatePropsDependOnOwnProps ? this.finalMapStateToProps(state, props) : this.finalMapStateToProps(state);
	
	        if (process.env.NODE_ENV !== 'production') {
	          checkStateShape(stateProps, 'mapStateToProps');
	        }
	        return stateProps;
	      };
	
	      Connect.prototype.configureFinalMapState = function configureFinalMapState(store, props) {
	        var mappedState = mapState(store.getState(), props);
	        var isFactory = typeof mappedState === 'function';
	
	        this.finalMapStateToProps = isFactory ? mappedState : mapState;
	        this.doStatePropsDependOnOwnProps = this.finalMapStateToProps.length !== 1;
	
	        if (isFactory) {
	          return this.computeStateProps(store, props);
	        }
	
	        if (process.env.NODE_ENV !== 'production') {
	          checkStateShape(mappedState, 'mapStateToProps');
	        }
	        return mappedState;
	      };
	
	      Connect.prototype.computeDispatchProps = function computeDispatchProps(store, props) {
	        if (!this.finalMapDispatchToProps) {
	          return this.configureFinalMapDispatch(store, props);
	        }
	
	        var dispatch = store.dispatch;
	
	        var dispatchProps = this.doDispatchPropsDependOnOwnProps ? this.finalMapDispatchToProps(dispatch, props) : this.finalMapDispatchToProps(dispatch);
	
	        if (process.env.NODE_ENV !== 'production') {
	          checkStateShape(dispatchProps, 'mapDispatchToProps');
	        }
	        return dispatchProps;
	      };
	
	      Connect.prototype.configureFinalMapDispatch = function configureFinalMapDispatch(store, props) {
	        var mappedDispatch = mapDispatch(store.dispatch, props);
	        var isFactory = typeof mappedDispatch === 'function';
	
	        this.finalMapDispatchToProps = isFactory ? mappedDispatch : mapDispatch;
	        this.doDispatchPropsDependOnOwnProps = this.finalMapDispatchToProps.length !== 1;
	
	        if (isFactory) {
	          return this.computeDispatchProps(store, props);
	        }
	
	        if (process.env.NODE_ENV !== 'production') {
	          checkStateShape(mappedDispatch, 'mapDispatchToProps');
	        }
	        return mappedDispatch;
	      };
	
	      Connect.prototype.updateStatePropsIfNeeded = function updateStatePropsIfNeeded() {
	        var nextStateProps = this.computeStateProps(this.store, this.props);
	        if (this.stateProps && (0, _shallowEqual2["default"])(nextStateProps, this.stateProps)) {
	          return false;
	        }
	
	        this.stateProps = nextStateProps;
	        return true;
	      };
	
	      Connect.prototype.updateDispatchPropsIfNeeded = function updateDispatchPropsIfNeeded() {
	        var nextDispatchProps = this.computeDispatchProps(this.store, this.props);
	        if (this.dispatchProps && (0, _shallowEqual2["default"])(nextDispatchProps, this.dispatchProps)) {
	          return false;
	        }
	
	        this.dispatchProps = nextDispatchProps;
	        return true;
	      };
	
	      Connect.prototype.updateMergedPropsIfNeeded = function updateMergedPropsIfNeeded() {
	        var nextMergedProps = computeMergedProps(this.stateProps, this.dispatchProps, this.props);
	        if (this.mergedProps && checkMergedEquals && (0, _shallowEqual2["default"])(nextMergedProps, this.mergedProps)) {
	          return false;
	        }
	
	        this.mergedProps = nextMergedProps;
	        return true;
	      };
	
	      Connect.prototype.isSubscribed = function isSubscribed() {
	        return typeof this.unsubscribe === 'function';
	      };
	
	      Connect.prototype.trySubscribe = function trySubscribe() {
	        if (shouldSubscribe && !this.unsubscribe) {
	          this.unsubscribe = this.store.subscribe(this.handleChange.bind(this));
	          this.handleChange();
	        }
	      };
	
	      Connect.prototype.tryUnsubscribe = function tryUnsubscribe() {
	        if (this.unsubscribe) {
	          this.unsubscribe();
	          this.unsubscribe = null;
	        }
	      };
	
	      Connect.prototype.componentDidMount = function componentDidMount() {
	        this.trySubscribe();
	      };
	
	      Connect.prototype.componentWillReceiveProps = function componentWillReceiveProps(nextProps) {
	        if (!pure || !(0, _shallowEqual2["default"])(nextProps, this.props)) {
	          this.haveOwnPropsChanged = true;
	        }
	      };
	
	      Connect.prototype.componentWillUnmount = function componentWillUnmount() {
	        this.tryUnsubscribe();
	        this.clearCache();
	      };
	
	      Connect.prototype.clearCache = function clearCache() {
	        this.dispatchProps = null;
	        this.stateProps = null;
	        this.mergedProps = null;
	        this.haveOwnPropsChanged = true;
	        this.hasStoreStateChanged = true;
	        this.haveStatePropsBeenPrecalculated = false;
	        this.statePropsPrecalculationError = null;
	        this.renderedElement = null;
	        this.finalMapDispatchToProps = null;
	        this.finalMapStateToProps = null;
	      };
	
	      Connect.prototype.handleChange = function handleChange() {
	        if (!this.unsubscribe) {
	          return;
	        }
	
	        var storeState = this.store.getState();
	        var prevStoreState = this.state.storeState;
	        if (pure && prevStoreState === storeState) {
	          return;
	        }
	
	        if (pure && !this.doStatePropsDependOnOwnProps) {
	          var haveStatePropsChanged = tryCatch(this.updateStatePropsIfNeeded, this);
	          if (!haveStatePropsChanged) {
	            return;
	          }
	          if (haveStatePropsChanged === errorObject) {
	            this.statePropsPrecalculationError = errorObject.value;
	          }
	          this.haveStatePropsBeenPrecalculated = true;
	        }
	
	        this.hasStoreStateChanged = true;
	        this.setState({ storeState: storeState });
	      };
	
	      Connect.prototype.getWrappedInstance = function getWrappedInstance() {
	        (0, _invariant2["default"])(withRef, 'To access the wrapped instance, you need to specify ' + '{ withRef: true } as the fourth argument of the connect() call.');
	
	        return this.refs.wrappedInstance;
	      };
	
	      Connect.prototype.render = function render() {
	        var haveOwnPropsChanged = this.haveOwnPropsChanged,
	            hasStoreStateChanged = this.hasStoreStateChanged,
	            haveStatePropsBeenPrecalculated = this.haveStatePropsBeenPrecalculated,
	            statePropsPrecalculationError = this.statePropsPrecalculationError,
	            renderedElement = this.renderedElement;
	
	
	        this.haveOwnPropsChanged = false;
	        this.hasStoreStateChanged = false;
	        this.haveStatePropsBeenPrecalculated = false;
	        this.statePropsPrecalculationError = null;
	
	        if (statePropsPrecalculationError) {
	          throw statePropsPrecalculationError;
	        }
	
	        var shouldUpdateStateProps = true;
	        var shouldUpdateDispatchProps = true;
	        if (pure && renderedElement) {
	          shouldUpdateStateProps = hasStoreStateChanged || haveOwnPropsChanged && this.doStatePropsDependOnOwnProps;
	          shouldUpdateDispatchProps = haveOwnPropsChanged && this.doDispatchPropsDependOnOwnProps;
	        }
	
	        var haveStatePropsChanged = false;
	        var haveDispatchPropsChanged = false;
	        if (haveStatePropsBeenPrecalculated) {
	          haveStatePropsChanged = true;
	        } else if (shouldUpdateStateProps) {
	          haveStatePropsChanged = this.updateStatePropsIfNeeded();
	        }
	        if (shouldUpdateDispatchProps) {
	          haveDispatchPropsChanged = this.updateDispatchPropsIfNeeded();
	        }
	
	        var haveMergedPropsChanged = true;
	        if (haveStatePropsChanged || haveDispatchPropsChanged || haveOwnPropsChanged) {
	          haveMergedPropsChanged = this.updateMergedPropsIfNeeded();
	        } else {
	          haveMergedPropsChanged = false;
	        }
	
	        if (!haveMergedPropsChanged && renderedElement) {
	          return renderedElement;
	        }
	
	        if (withRef) {
	          this.renderedElement = (0, _react.createElement)(WrappedComponent, _extends({}, this.mergedProps, {
	            ref: 'wrappedInstance'
	          }));
	        } else {
	          this.renderedElement = (0, _react.createElement)(WrappedComponent, this.mergedProps);
	        }
	
	        return this.renderedElement;
	      };
	
	      return Connect;
	    }(_react.Component);
	
	    Connect.displayName = connectDisplayName;
	    Connect.WrappedComponent = WrappedComponent;
	    Connect.contextTypes = {
	      store: _storeShape2["default"]
	    };
	    Connect.propTypes = {
	      store: _storeShape2["default"]
	    };
	
	    if (process.env.NODE_ENV !== 'production') {
	      Connect.prototype.componentWillUpdate = function componentWillUpdate() {
	        if (this.version === version) {
	          return;
	        }
	
	        // We are hot reloading!
	        this.version = version;
	        this.trySubscribe();
	        this.clearCache();
	      };
	    }
	
	    return (0, _hoistNonReactStatics2["default"])(Connect, WrappedComponent);
	  };
	}
	/* WEBPACK VAR INJECTION */}.call(exports, __webpack_require__(5)))

/***/ },
/* 9 */
/***/ function(module, exports) {

	"use strict";
	
	exports.__esModule = true;
	exports["default"] = shallowEqual;
	function shallowEqual(objA, objB) {
	  if (objA === objB) {
	    return true;
	  }
	
	  var keysA = Object.keys(objA);
	  var keysB = Object.keys(objB);
	
	  if (keysA.length !== keysB.length) {
	    return false;
	  }
	
	  // Test for A's keys different from B.
	  var hasOwn = Object.prototype.hasOwnProperty;
	  for (var i = 0; i < keysA.length; i++) {
	    if (!hasOwn.call(objB, keysA[i]) || objA[keysA[i]] !== objB[keysA[i]]) {
	      return false;
	    }
	  }
	
	  return true;
	}

/***/ },
/* 10 */
/***/ function(module, exports, __webpack_require__) {

	'use strict';
	
	exports.__esModule = true;
	exports["default"] = wrapActionCreators;
	
	var _redux = __webpack_require__(11);
	
	function wrapActionCreators(actionCreators) {
	  return function (dispatch) {
	    return (0, _redux.bindActionCreators)(actionCreators, dispatch);
	  };
	}

/***/ },
/* 11 */
/***/ function(module, exports, __webpack_require__) {

	/* WEBPACK VAR INJECTION */(function(process) {'use strict';
	
	exports.__esModule = true;
	exports.compose = exports.applyMiddleware = exports.bindActionCreators = exports.combineReducers = exports.createStore = undefined;
	
	var _createStore = __webpack_require__(12);
	
	var _createStore2 = _interopRequireDefault(_createStore);
	
	var _combineReducers = __webpack_require__(27);
	
	var _combineReducers2 = _interopRequireDefault(_combineReducers);
	
	var _bindActionCreators = __webpack_require__(29);
	
	var _bindActionCreators2 = _interopRequireDefault(_bindActionCreators);
	
	var _applyMiddleware = __webpack_require__(30);
	
	var _applyMiddleware2 = _interopRequireDefault(_applyMiddleware);
	
	var _compose = __webpack_require__(31);
	
	var _compose2 = _interopRequireDefault(_compose);
	
	var _warning = __webpack_require__(28);
	
	var _warning2 = _interopRequireDefault(_warning);
	
	function _interopRequireDefault(obj) { return obj && obj.__esModule ? obj : { 'default': obj }; }
	
	/*
	* This is a dummy function to check if the function name has been altered by minification.
	* If the function has been minified and NODE_ENV !== 'production', warn the user.
	*/
	function isCrushed() {}
	
	if (process.env.NODE_ENV !== 'production' && typeof isCrushed.name === 'string' && isCrushed.name !== 'isCrushed') {
	  (0, _warning2['default'])('You are currently using minified code outside of NODE_ENV === \'production\'. ' + 'This means that you are running a slower development build of Redux. ' + 'You can use loose-envify (https://github.com/zertosh/loose-envify) for browserify ' + 'or DefinePlugin for webpack (http://stackoverflow.com/questions/30030031) ' + 'to ensure you have the correct code for your production build.');
	}
	
	exports.createStore = _createStore2['default'];
	exports.combineReducers = _combineReducers2['default'];
	exports.bindActionCreators = _bindActionCreators2['default'];
	exports.applyMiddleware = _applyMiddleware2['default'];
	exports.compose = _compose2['default'];
	/* WEBPACK VAR INJECTION */}.call(exports, __webpack_require__(5)))

/***/ },
/* 12 */
/***/ function(module, exports, __webpack_require__) {

	'use strict';
	
	exports.__esModule = true;
	exports.ActionTypes = undefined;
	exports['default'] = createStore;
	
	var _isPlainObject = __webpack_require__(13);
	
	var _isPlainObject2 = _interopRequireDefault(_isPlainObject);
	
	var _symbolObservable = __webpack_require__(23);
	
	var _symbolObservable2 = _interopRequireDefault(_symbolObservable);
	
	function _interopRequireDefault(obj) { return obj && obj.__esModule ? obj : { 'default': obj }; }
	
	/**
	 * These are private action types reserved by Redux.
	 * For any unknown actions, you must return the current state.
	 * If the current state is undefined, you must return the initial state.
	 * Do not reference these action types directly in your code.
	 */
	var ActionTypes = exports.ActionTypes = {
	  INIT: '@@redux/INIT'
	};
	
	/**
	 * Creates a Redux store that holds the state tree.
	 * The only way to change the data in the store is to call `dispatch()` on it.
	 *
	 * There should only be a single store in your app. To specify how different
	 * parts of the state tree respond to actions, you may combine several reducers
	 * into a single reducer function by using `combineReducers`.
	 *
	 * @param {Function} reducer A function that returns the next state tree, given
	 * the current state tree and the action to handle.
	 *
	 * @param {any} [preloadedState] The initial state. You may optionally specify it
	 * to hydrate the state from the server in universal apps, or to restore a
	 * previously serialized user session.
	 * If you use `combineReducers` to produce the root reducer function, this must be
	 * an object with the same shape as `combineReducers` keys.
	 *
	 * @param {Function} enhancer The store enhancer. You may optionally specify it
	 * to enhance the store with third-party capabilities such as middleware,
	 * time travel, persistence, etc. The only store enhancer that ships with Redux
	 * is `applyMiddleware()`.
	 *
	 * @returns {Store} A Redux store that lets you read the state, dispatch actions
	 * and subscribe to changes.
	 */
	function createStore(reducer, preloadedState, enhancer) {
	  var _ref2;
	
	  if (typeof preloadedState === 'function' && typeof enhancer === 'undefined') {
	    enhancer = preloadedState;
	    preloadedState = undefined;
	  }
	
	  if (typeof enhancer !== 'undefined') {
	    if (typeof enhancer !== 'function') {
	      throw new Error('Expected the enhancer to be a function.');
	    }
	
	    return enhancer(createStore)(reducer, preloadedState);
	  }
	
	  if (typeof reducer !== 'function') {
	    throw new Error('Expected the reducer to be a function.');
	  }
	
	  var currentReducer = reducer;
	  var currentState = preloadedState;
	  var currentListeners = [];
	  var nextListeners = currentListeners;
	  var isDispatching = false;
	
	  function ensureCanMutateNextListeners() {
	    if (nextListeners === currentListeners) {
	      nextListeners = currentListeners.slice();
	    }
	  }
	
	  /**
	   * Reads the state tree managed by the store.
	   *
	   * @returns {any} The current state tree of your application.
	   */
	  function getState() {
	    return currentState;
	  }
	
	  /**
	   * Adds a change listener. It will be called any time an action is dispatched,
	   * and some part of the state tree may potentially have changed. You may then
	   * call `getState()` to read the current state tree inside the callback.
	   *
	   * You may call `dispatch()` from a change listener, with the following
	   * caveats:
	   *
	   * 1. The subscriptions are snapshotted just before every `dispatch()` call.
	   * If you subscribe or unsubscribe while the listeners are being invoked, this
	   * will not have any effect on the `dispatch()` that is currently in progress.
	   * However, the next `dispatch()` call, whether nested or not, will use a more
	   * recent snapshot of the subscription list.
	   *
	   * 2. The listener should not expect to see all state changes, as the state
	   * might have been updated multiple times during a nested `dispatch()` before
	   * the listener is called. It is, however, guaranteed that all subscribers
	   * registered before the `dispatch()` started will be called with the latest
	   * state by the time it exits.
	   *
	   * @param {Function} listener A callback to be invoked on every dispatch.
	   * @returns {Function} A function to remove this change listener.
	   */
	  function subscribe(listener) {
	    if (typeof listener !== 'function') {
	      throw new Error('Expected listener to be a function.');
	    }
	
	    var isSubscribed = true;
	
	    ensureCanMutateNextListeners();
	    nextListeners.push(listener);
	
	    return function unsubscribe() {
	      if (!isSubscribed) {
	        return;
	      }
	
	      isSubscribed = false;
	
	      ensureCanMutateNextListeners();
	      var index = nextListeners.indexOf(listener);
	      nextListeners.splice(index, 1);
	    };
	  }
	
	  /**
	   * Dispatches an action. It is the only way to trigger a state change.
	   *
	   * The `reducer` function, used to create the store, will be called with the
	   * current state tree and the given `action`. Its return value will
	   * be considered the **next** state of the tree, and the change listeners
	   * will be notified.
	   *
	   * The base implementation only supports plain object actions. If you want to
	   * dispatch a Promise, an Observable, a thunk, or something else, you need to
	   * wrap your store creating function into the corresponding middleware. For
	   * example, see the documentation for the `redux-thunk` package. Even the
	   * middleware will eventually dispatch plain object actions using this method.
	   *
	   * @param {Object} action A plain object representing “what changed”. It is
	   * a good idea to keep actions serializable so you can record and replay user
	   * sessions, or use the time travelling `redux-devtools`. An action must have
	   * a `type` property which may not be `undefined`. It is a good idea to use
	   * string constants for action types.
	   *
	   * @returns {Object} For convenience, the same action object you dispatched.
	   *
	   * Note that, if you use a custom middleware, it may wrap `dispatch()` to
	   * return something else (for example, a Promise you can await).
	   */
	  function dispatch(action) {
	    if (!(0, _isPlainObject2['default'])(action)) {
	      throw new Error('Actions must be plain objects. ' + 'Use custom middleware for async actions.');
	    }
	
	    if (typeof action.type === 'undefined') {
	      throw new Error('Actions may not have an undefined "type" property. ' + 'Have you misspelled a constant?');
	    }
	
	    if (isDispatching) {
	      throw new Error('Reducers may not dispatch actions.');
	    }
	
	    try {
	      isDispatching = true;
	      currentState = currentReducer(currentState, action);
	    } finally {
	      isDispatching = false;
	    }
	
	    var listeners = currentListeners = nextListeners;
	    for (var i = 0; i < listeners.length; i++) {
	      listeners[i]();
	    }
	
	    return action;
	  }
	
	  /**
	   * Replaces the reducer currently used by the store to calculate the state.
	   *
	   * You might need this if your app implements code splitting and you want to
	   * load some of the reducers dynamically. You might also need this if you
	   * implement a hot reloading mechanism for Redux.
	   *
	   * @param {Function} nextReducer The reducer for the store to use instead.
	   * @returns {void}
	   */
	  function replaceReducer(nextReducer) {
	    if (typeof nextReducer !== 'function') {
	      throw new Error('Expected the nextReducer to be a function.');
	    }
	
	    currentReducer = nextReducer;
	    dispatch({ type: ActionTypes.INIT });
	  }
	
	  /**
	   * Interoperability point for observable/reactive libraries.
	   * @returns {observable} A minimal observable of state changes.
	   * For more information, see the observable proposal:
	   * https://github.com/zenparsing/es-observable
	   */
	  function observable() {
	    var _ref;
	
	    var outerSubscribe = subscribe;
	    return _ref = {
	      /**
	       * The minimal observable subscription method.
	       * @param {Object} observer Any object that can be used as an observer.
	       * The observer object should have a `next` method.
	       * @returns {subscription} An object with an `unsubscribe` method that can
	       * be used to unsubscribe the observable from the store, and prevent further
	       * emission of values from the observable.
	       */
	      subscribe: function subscribe(observer) {
	        if (typeof observer !== 'object') {
	          throw new TypeError('Expected the observer to be an object.');
	        }
	
	        function observeState() {
	          if (observer.next) {
	            observer.next(getState());
	          }
	        }
	
	        observeState();
	        var unsubscribe = outerSubscribe(observeState);
	        return { unsubscribe: unsubscribe };
	      }
	    }, _ref[_symbolObservable2['default']] = function () {
	      return this;
	    }, _ref;
	  }
	
	  // When a store is created, an "INIT" action is dispatched so that every
	  // reducer returns their initial state. This effectively populates
	  // the initial state tree.
	  dispatch({ type: ActionTypes.INIT });
	
	  return _ref2 = {
	    dispatch: dispatch,
	    subscribe: subscribe,
	    getState: getState,
	    replaceReducer: replaceReducer
	  }, _ref2[_symbolObservable2['default']] = observable, _ref2;
	}

/***/ },
/* 13 */
/***/ function(module, exports, __webpack_require__) {

	var baseGetTag = __webpack_require__(14),
	    getPrototype = __webpack_require__(20),
	    isObjectLike = __webpack_require__(22);
	
	/** `Object#toString` result references. */
	var objectTag = '[object Object]';
	
	/** Used for built-in method references. */
	var funcProto = Function.prototype,
	    objectProto = Object.prototype;
	
	/** Used to resolve the decompiled source of functions. */
	var funcToString = funcProto.toString;
	
	/** Used to check objects for own properties. */
	var hasOwnProperty = objectProto.hasOwnProperty;
	
	/** Used to infer the `Object` constructor. */
	var objectCtorString = funcToString.call(Object);
	
	/**
	 * Checks if `value` is a plain object, that is, an object created by the
	 * `Object` constructor or one with a `[[Prototype]]` of `null`.
	 *
	 * @static
	 * @memberOf _
	 * @since 0.8.0
	 * @category Lang
	 * @param {*} value The value to check.
	 * @returns {boolean} Returns `true` if `value` is a plain object, else `false`.
	 * @example
	 *
	 * function Foo() {
	 *   this.a = 1;
	 * }
	 *
	 * _.isPlainObject(new Foo);
	 * // => false
	 *
	 * _.isPlainObject([1, 2, 3]);
	 * // => false
	 *
	 * _.isPlainObject({ 'x': 0, 'y': 0 });
	 * // => true
	 *
	 * _.isPlainObject(Object.create(null));
	 * // => true
	 */
	function isPlainObject(value) {
	  if (!isObjectLike(value) || baseGetTag(value) != objectTag) {
	    return false;
	  }
	  var proto = getPrototype(value);
	  if (proto === null) {
	    return true;
	  }
	  var Ctor = hasOwnProperty.call(proto, 'constructor') && proto.constructor;
	  return typeof Ctor == 'function' && Ctor instanceof Ctor &&
	    funcToString.call(Ctor) == objectCtorString;
	}
	
	module.exports = isPlainObject;


/***/ },
/* 14 */
/***/ function(module, exports, __webpack_require__) {

	var Symbol = __webpack_require__(15),
	    getRawTag = __webpack_require__(18),
	    objectToString = __webpack_require__(19);
	
	/** `Object#toString` result references. */
	var nullTag = '[object Null]',
	    undefinedTag = '[object Undefined]';
	
	/** Built-in value references. */
	var symToStringTag = Symbol ? Symbol.toStringTag : undefined;
	
	/**
	 * The base implementation of `getTag` without fallbacks for buggy environments.
	 *
	 * @private
	 * @param {*} value The value to query.
	 * @returns {string} Returns the `toStringTag`.
	 */
	function baseGetTag(value) {
	  if (value == null) {
	    return value === undefined ? undefinedTag : nullTag;
	  }
	  return (symToStringTag && symToStringTag in Object(value))
	    ? getRawTag(value)
	    : objectToString(value);
	}
	
	module.exports = baseGetTag;


/***/ },
/* 15 */
/***/ function(module, exports, __webpack_require__) {

	var root = __webpack_require__(16);
	
	/** Built-in value references. */
	var Symbol = root.Symbol;
	
	module.exports = Symbol;


/***/ },
/* 16 */
/***/ function(module, exports, __webpack_require__) {

	var freeGlobal = __webpack_require__(17);
	
	/** Detect free variable `self`. */
	var freeSelf = typeof self == 'object' && self && self.Object === Object && self;
	
	/** Used as a reference to the global object. */
	var root = freeGlobal || freeSelf || Function('return this')();
	
	module.exports = root;


/***/ },
/* 17 */
/***/ function(module, exports) {

	/* WEBPACK VAR INJECTION */(function(global) {/** Detect free variable `global` from Node.js. */
	var freeGlobal = typeof global == 'object' && global && global.Object === Object && global;
	
	module.exports = freeGlobal;
	
	/* WEBPACK VAR INJECTION */}.call(exports, (function() { return this; }())))

/***/ },
/* 18 */
/***/ function(module, exports, __webpack_require__) {

	var Symbol = __webpack_require__(15);
	
	/** Used for built-in method references. */
	var objectProto = Object.prototype;
	
	/** Used to check objects for own properties. */
	var hasOwnProperty = objectProto.hasOwnProperty;
	
	/**
	 * Used to resolve the
	 * [`toStringTag`](http://ecma-international.org/ecma-262/7.0/#sec-object.prototype.tostring)
	 * of values.
	 */
	var nativeObjectToString = objectProto.toString;
	
	/** Built-in value references. */
	var symToStringTag = Symbol ? Symbol.toStringTag : undefined;
	
	/**
	 * A specialized version of `baseGetTag` which ignores `Symbol.toStringTag` values.
	 *
	 * @private
	 * @param {*} value The value to query.
	 * @returns {string} Returns the raw `toStringTag`.
	 */
	function getRawTag(value) {
	  var isOwn = hasOwnProperty.call(value, symToStringTag),
	      tag = value[symToStringTag];
	
	  try {
	    value[symToStringTag] = undefined;
	    var unmasked = true;
	  } catch (e) {}
	
	  var result = nativeObjectToString.call(value);
	  if (unmasked) {
	    if (isOwn) {
	      value[symToStringTag] = tag;
	    } else {
	      delete value[symToStringTag];
	    }
	  }
	  return result;
	}
	
	module.exports = getRawTag;


/***/ },
/* 19 */
/***/ function(module, exports) {

	/** Used for built-in method references. */
	var objectProto = Object.prototype;
	
	/**
	 * Used to resolve the
	 * [`toStringTag`](http://ecma-international.org/ecma-262/7.0/#sec-object.prototype.tostring)
	 * of values.
	 */
	var nativeObjectToString = objectProto.toString;
	
	/**
	 * Converts `value` to a string using `Object.prototype.toString`.
	 *
	 * @private
	 * @param {*} value The value to convert.
	 * @returns {string} Returns the converted string.
	 */
	function objectToString(value) {
	  return nativeObjectToString.call(value);
	}
	
	module.exports = objectToString;


/***/ },
/* 20 */
/***/ function(module, exports, __webpack_require__) {

	var overArg = __webpack_require__(21);
	
	/** Built-in value references. */
	var getPrototype = overArg(Object.getPrototypeOf, Object);
	
	module.exports = getPrototype;


/***/ },
/* 21 */
/***/ function(module, exports) {

	/**
	 * Creates a unary function that invokes `func` with its argument transformed.
	 *
	 * @private
	 * @param {Function} func The function to wrap.
	 * @param {Function} transform The argument transform.
	 * @returns {Function} Returns the new function.
	 */
	function overArg(func, transform) {
	  return function(arg) {
	    return func(transform(arg));
	  };
	}
	
	module.exports = overArg;


/***/ },
/* 22 */
/***/ function(module, exports) {

	/**
	 * Checks if `value` is object-like. A value is object-like if it's not `null`
	 * and has a `typeof` result of "object".
	 *
	 * @static
	 * @memberOf _
	 * @since 4.0.0
	 * @category Lang
	 * @param {*} value The value to check.
	 * @returns {boolean} Returns `true` if `value` is object-like, else `false`.
	 * @example
	 *
	 * _.isObjectLike({});
	 * // => true
	 *
	 * _.isObjectLike([1, 2, 3]);
	 * // => true
	 *
	 * _.isObjectLike(_.noop);
	 * // => false
	 *
	 * _.isObjectLike(null);
	 * // => false
	 */
	function isObjectLike(value) {
	  return value != null && typeof value == 'object';
	}
	
	module.exports = isObjectLike;


/***/ },
/* 23 */
/***/ function(module, exports, __webpack_require__) {

	module.exports = __webpack_require__(24);


/***/ },
/* 24 */
/***/ function(module, exports, __webpack_require__) {

	/* WEBPACK VAR INJECTION */(function(global, module) {'use strict';
	
	Object.defineProperty(exports, "__esModule", {
	  value: true
	});
	
	var _ponyfill = __webpack_require__(26);
	
	var _ponyfill2 = _interopRequireDefault(_ponyfill);
	
	function _interopRequireDefault(obj) { return obj && obj.__esModule ? obj : { 'default': obj }; }
	
	var root; /* global window */
	
	
	if (typeof self !== 'undefined') {
	  root = self;
	} else if (typeof window !== 'undefined') {
	  root = window;
	} else if (typeof global !== 'undefined') {
	  root = global;
	} else if (true) {
	  root = module;
	} else {
	  root = Function('return this')();
	}
	
	var result = (0, _ponyfill2['default'])(root);
	exports['default'] = result;
	/* WEBPACK VAR INJECTION */}.call(exports, (function() { return this; }()), __webpack_require__(25)(module)))

/***/ },
/* 25 */
/***/ function(module, exports) {

	module.exports = function(module) {
		if(!module.webpackPolyfill) {
			module.deprecate = function() {};
			module.paths = [];
			// module.parent = undefined by default
			module.children = [];
			module.webpackPolyfill = 1;
		}
		return module;
	}


/***/ },
/* 26 */
/***/ function(module, exports) {

	'use strict';
	
	Object.defineProperty(exports, "__esModule", {
		value: true
	});
	exports['default'] = symbolObservablePonyfill;
	function symbolObservablePonyfill(root) {
		var result;
		var _Symbol = root.Symbol;
	
		if (typeof _Symbol === 'function') {
			if (_Symbol.observable) {
				result = _Symbol.observable;
			} else {
				result = _Symbol('observable');
				_Symbol.observable = result;
			}
		} else {
			result = '@@observable';
		}
	
		return result;
	};

/***/ },
/* 27 */
/***/ function(module, exports, __webpack_require__) {

	/* WEBPACK VAR INJECTION */(function(process) {'use strict';
	
	exports.__esModule = true;
	exports['default'] = combineReducers;
	
	var _createStore = __webpack_require__(12);
	
	var _isPlainObject = __webpack_require__(13);
	
	var _isPlainObject2 = _interopRequireDefault(_isPlainObject);
	
	var _warning = __webpack_require__(28);
	
	var _warning2 = _interopRequireDefault(_warning);
	
	function _interopRequireDefault(obj) { return obj && obj.__esModule ? obj : { 'default': obj }; }
	
	function getUndefinedStateErrorMessage(key, action) {
	  var actionType = action && action.type;
	  var actionName = actionType && '"' + actionType.toString() + '"' || 'an action';
	
	  return 'Given action ' + actionName + ', reducer "' + key + '" returned undefined. ' + 'To ignore an action, you must explicitly return the previous state.';
	}
	
	function getUnexpectedStateShapeWarningMessage(inputState, reducers, action, unexpectedKeyCache) {
	  var reducerKeys = Object.keys(reducers);
	  var argumentName = action && action.type === _createStore.ActionTypes.INIT ? 'preloadedState argument passed to createStore' : 'previous state received by the reducer';
	
	  if (reducerKeys.length === 0) {
	    return 'Store does not have a valid reducer. Make sure the argument passed ' + 'to combineReducers is an object whose values are reducers.';
	  }
	
	  if (!(0, _isPlainObject2['default'])(inputState)) {
	    return 'The ' + argumentName + ' has unexpected type of "' + {}.toString.call(inputState).match(/\s([a-z|A-Z]+)/)[1] + '". Expected argument to be an object with the following ' + ('keys: "' + reducerKeys.join('", "') + '"');
	  }
	
	  var unexpectedKeys = Object.keys(inputState).filter(function (key) {
	    return !reducers.hasOwnProperty(key) && !unexpectedKeyCache[key];
	  });
	
	  unexpectedKeys.forEach(function (key) {
	    unexpectedKeyCache[key] = true;
	  });
	
	  if (unexpectedKeys.length > 0) {
	    return 'Unexpected ' + (unexpectedKeys.length > 1 ? 'keys' : 'key') + ' ' + ('"' + unexpectedKeys.join('", "') + '" found in ' + argumentName + '. ') + 'Expected to find one of the known reducer keys instead: ' + ('"' + reducerKeys.join('", "') + '". Unexpected keys will be ignored.');
	  }
	}
	
	function assertReducerSanity(reducers) {
	  Object.keys(reducers).forEach(function (key) {
	    var reducer = reducers[key];
	    var initialState = reducer(undefined, { type: _createStore.ActionTypes.INIT });
	
	    if (typeof initialState === 'undefined') {
	      throw new Error('Reducer "' + key + '" returned undefined during initialization. ' + 'If the state passed to the reducer is undefined, you must ' + 'explicitly return the initial state. The initial state may ' + 'not be undefined.');
	    }
	
	    var type = '@@redux/PROBE_UNKNOWN_ACTION_' + Math.random().toString(36).substring(7).split('').join('.');
	    if (typeof reducer(undefined, { type: type }) === 'undefined') {
	      throw new Error('Reducer "' + key + '" returned undefined when probed with a random type. ' + ('Don\'t try to handle ' + _createStore.ActionTypes.INIT + ' or other actions in "redux/*" ') + 'namespace. They are considered private. Instead, you must return the ' + 'current state for any unknown actions, unless it is undefined, ' + 'in which case you must return the initial state, regardless of the ' + 'action type. The initial state may not be undefined.');
	    }
	  });
	}
	
	/**
	 * Turns an object whose values are different reducer functions, into a single
	 * reducer function. It will call every child reducer, and gather their results
	 * into a single state object, whose keys correspond to the keys of the passed
	 * reducer functions.
	 *
	 * @param {Object} reducers An object whose values correspond to different
	 * reducer functions that need to be combined into one. One handy way to obtain
	 * it is to use ES6 `import * as reducers` syntax. The reducers may never return
	 * undefined for any action. Instead, they should return their initial state
	 * if the state passed to them was undefined, and the current state for any
	 * unrecognized action.
	 *
	 * @returns {Function} A reducer function that invokes every reducer inside the
	 * passed object, and builds a state object with the same shape.
	 */
	function combineReducers(reducers) {
	  var reducerKeys = Object.keys(reducers);
	  var finalReducers = {};
	  for (var i = 0; i < reducerKeys.length; i++) {
	    var key = reducerKeys[i];
	
	    if (process.env.NODE_ENV !== 'production') {
	      if (typeof reducers[key] === 'undefined') {
	        (0, _warning2['default'])('No reducer provided for key "' + key + '"');
	      }
	    }
	
	    if (typeof reducers[key] === 'function') {
	      finalReducers[key] = reducers[key];
	    }
	  }
	  var finalReducerKeys = Object.keys(finalReducers);
	
	  if (process.env.NODE_ENV !== 'production') {
	    var unexpectedKeyCache = {};
	  }
	
	  var sanityError;
	  try {
	    assertReducerSanity(finalReducers);
	  } catch (e) {
	    sanityError = e;
	  }
	
	  return function combination() {
	    var state = arguments.length <= 0 || arguments[0] === undefined ? {} : arguments[0];
	    var action = arguments[1];
	
	    if (sanityError) {
	      throw sanityError;
	    }
	
	    if (process.env.NODE_ENV !== 'production') {
	      var warningMessage = getUnexpectedStateShapeWarningMessage(state, finalReducers, action, unexpectedKeyCache);
	      if (warningMessage) {
	        (0, _warning2['default'])(warningMessage);
	      }
	    }
	
	    var hasChanged = false;
	    var nextState = {};
	    for (var i = 0; i < finalReducerKeys.length; i++) {
	      var key = finalReducerKeys[i];
	      var reducer = finalReducers[key];
	      var previousStateForKey = state[key];
	      var nextStateForKey = reducer(previousStateForKey, action);
	      if (typeof nextStateForKey === 'undefined') {
	        var errorMessage = getUndefinedStateErrorMessage(key, action);
	        throw new Error(errorMessage);
	      }
	      nextState[key] = nextStateForKey;
	      hasChanged = hasChanged || nextStateForKey !== previousStateForKey;
	    }
	    return hasChanged ? nextState : state;
	  };
	}
	/* WEBPACK VAR INJECTION */}.call(exports, __webpack_require__(5)))

/***/ },
/* 28 */
/***/ function(module, exports) {

	'use strict';
	
	exports.__esModule = true;
	exports['default'] = warning;
	/**
	 * Prints a warning in the console if it exists.
	 *
	 * @param {String} message The warning message.
	 * @returns {void}
	 */
	function warning(message) {
	  /* eslint-disable no-console */
	  if (typeof console !== 'undefined' && typeof console.error === 'function') {
	    console.error(message);
	  }
	  /* eslint-enable no-console */
	  try {
	    // This error was thrown as a convenience so that if you enable
	    // "break on all exceptions" in your console,
	    // it would pause the execution at this line.
	    throw new Error(message);
	    /* eslint-disable no-empty */
	  } catch (e) {}
	  /* eslint-enable no-empty */
	}

/***/ },
/* 29 */
/***/ function(module, exports) {

	'use strict';
	
	exports.__esModule = true;
	exports['default'] = bindActionCreators;
	function bindActionCreator(actionCreator, dispatch) {
	  return function () {
	    return dispatch(actionCreator.apply(undefined, arguments));
	  };
	}
	
	/**
	 * Turns an object whose values are action creators, into an object with the
	 * same keys, but with every function wrapped into a `dispatch` call so they
	 * may be invoked directly. This is just a convenience method, as you can call
	 * `store.dispatch(MyActionCreators.doSomething())` yourself just fine.
	 *
	 * For convenience, you can also pass a single function as the first argument,
	 * and get a function in return.
	 *
	 * @param {Function|Object} actionCreators An object whose values are action
	 * creator functions. One handy way to obtain it is to use ES6 `import * as`
	 * syntax. You may also pass a single function.
	 *
	 * @param {Function} dispatch The `dispatch` function available on your Redux
	 * store.
	 *
	 * @returns {Function|Object} The object mimicking the original object, but with
	 * every action creator wrapped into the `dispatch` call. If you passed a
	 * function as `actionCreators`, the return value will also be a single
	 * function.
	 */
	function bindActionCreators(actionCreators, dispatch) {
	  if (typeof actionCreators === 'function') {
	    return bindActionCreator(actionCreators, dispatch);
	  }
	
	  if (typeof actionCreators !== 'object' || actionCreators === null) {
	    throw new Error('bindActionCreators expected an object or a function, instead received ' + (actionCreators === null ? 'null' : typeof actionCreators) + '. ' + 'Did you write "import ActionCreators from" instead of "import * as ActionCreators from"?');
	  }
	
	  var keys = Object.keys(actionCreators);
	  var boundActionCreators = {};
	  for (var i = 0; i < keys.length; i++) {
	    var key = keys[i];
	    var actionCreator = actionCreators[key];
	    if (typeof actionCreator === 'function') {
	      boundActionCreators[key] = bindActionCreator(actionCreator, dispatch);
	    }
	  }
	  return boundActionCreators;
	}

/***/ },
/* 30 */
/***/ function(module, exports, __webpack_require__) {

	'use strict';
	
	exports.__esModule = true;
	
	var _extends = Object.assign || function (target) { for (var i = 1; i < arguments.length; i++) { var source = arguments[i]; for (var key in source) { if (Object.prototype.hasOwnProperty.call(source, key)) { target[key] = source[key]; } } } return target; };
	
	exports['default'] = applyMiddleware;
	
	var _compose = __webpack_require__(31);
	
	var _compose2 = _interopRequireDefault(_compose);
	
	function _interopRequireDefault(obj) { return obj && obj.__esModule ? obj : { 'default': obj }; }
	
	/**
	 * Creates a store enhancer that applies middleware to the dispatch method
	 * of the Redux store. This is handy for a variety of tasks, such as expressing
	 * asynchronous actions in a concise manner, or logging every action payload.
	 *
	 * See `redux-thunk` package as an example of the Redux middleware.
	 *
	 * Because middleware is potentially asynchronous, this should be the first
	 * store enhancer in the composition chain.
	 *
	 * Note that each middleware will be given the `dispatch` and `getState` functions
	 * as named arguments.
	 *
	 * @param {...Function} middlewares The middleware chain to be applied.
	 * @returns {Function} A store enhancer applying the middleware.
	 */
	function applyMiddleware() {
	  for (var _len = arguments.length, middlewares = Array(_len), _key = 0; _key < _len; _key++) {
	    middlewares[_key] = arguments[_key];
	  }
	
	  return function (createStore) {
	    return function (reducer, preloadedState, enhancer) {
	      var store = createStore(reducer, preloadedState, enhancer);
	      var _dispatch = store.dispatch;
	      var chain = [];
	
	      var middlewareAPI = {
	        getState: store.getState,
	        dispatch: function dispatch(action) {
	          return _dispatch(action);
	        }
	      };
	      chain = middlewares.map(function (middleware) {
	        return middleware(middlewareAPI);
	      });
	      _dispatch = _compose2['default'].apply(undefined, chain)(store.dispatch);
	
	      return _extends({}, store, {
	        dispatch: _dispatch
	      });
	    };
	  };
	}

/***/ },
/* 31 */
/***/ function(module, exports) {

	"use strict";
	
	exports.__esModule = true;
	exports["default"] = compose;
	/**
	 * Composes single-argument functions from right to left. The rightmost
	 * function can take multiple arguments as it provides the signature for
	 * the resulting composite function.
	 *
	 * @param {...Function} funcs The functions to compose.
	 * @returns {Function} A function obtained by composing the argument functions
	 * from right to left. For example, compose(f, g, h) is identical to doing
	 * (...args) => f(g(h(...args))).
	 */
	
	function compose() {
	  for (var _len = arguments.length, funcs = Array(_len), _key = 0; _key < _len; _key++) {
	    funcs[_key] = arguments[_key];
	  }
	
	  if (funcs.length === 0) {
	    return function (arg) {
	      return arg;
	    };
	  }
	
	  if (funcs.length === 1) {
	    return funcs[0];
	  }
	
	  var last = funcs[funcs.length - 1];
	  var rest = funcs.slice(0, -1);
	  return function () {
	    return rest.reduceRight(function (composed, f) {
	      return f(composed);
	    }, last.apply(undefined, arguments));
	  };
	}

/***/ },
/* 32 */
/***/ function(module, exports, __webpack_require__) {

	var baseGetTag = __webpack_require__(33),
	    getPrototype = __webpack_require__(39),
	    isObjectLike = __webpack_require__(41);
	
	/** `Object#toString` result references. */
	var objectTag = '[object Object]';
	
	/** Used for built-in method references. */
	var funcProto = Function.prototype,
	    objectProto = Object.prototype;
	
	/** Used to resolve the decompiled source of functions. */
	var funcToString = funcProto.toString;
	
	/** Used to check objects for own properties. */
	var hasOwnProperty = objectProto.hasOwnProperty;
	
	/** Used to infer the `Object` constructor. */
	var objectCtorString = funcToString.call(Object);
	
	/**
	 * Checks if `value` is a plain object, that is, an object created by the
	 * `Object` constructor or one with a `[[Prototype]]` of `null`.
	 *
	 * @static
	 * @memberOf _
	 * @since 0.8.0
	 * @category Lang
	 * @param {*} value The value to check.
	 * @returns {boolean} Returns `true` if `value` is a plain object, else `false`.
	 * @example
	 *
	 * function Foo() {
	 *   this.a = 1;
	 * }
	 *
	 * _.isPlainObject(new Foo);
	 * // => false
	 *
	 * _.isPlainObject([1, 2, 3]);
	 * // => false
	 *
	 * _.isPlainObject({ 'x': 0, 'y': 0 });
	 * // => true
	 *
	 * _.isPlainObject(Object.create(null));
	 * // => true
	 */
	function isPlainObject(value) {
	  if (!isObjectLike(value) || baseGetTag(value) != objectTag) {
	    return false;
	  }
	  var proto = getPrototype(value);
	  if (proto === null) {
	    return true;
	  }
	  var Ctor = hasOwnProperty.call(proto, 'constructor') && proto.constructor;
	  return typeof Ctor == 'function' && Ctor instanceof Ctor &&
	    funcToString.call(Ctor) == objectCtorString;
	}
	
	module.exports = isPlainObject;


/***/ },
/* 33 */
/***/ function(module, exports, __webpack_require__) {

	var Symbol = __webpack_require__(34),
	    getRawTag = __webpack_require__(37),
	    objectToString = __webpack_require__(38);
	
	/** `Object#toString` result references. */
	var nullTag = '[object Null]',
	    undefinedTag = '[object Undefined]';
	
	/** Built-in value references. */
	var symToStringTag = Symbol ? Symbol.toStringTag : undefined;
	
	/**
	 * The base implementation of `getTag` without fallbacks for buggy environments.
	 *
	 * @private
	 * @param {*} value The value to query.
	 * @returns {string} Returns the `toStringTag`.
	 */
	function baseGetTag(value) {
	  if (value == null) {
	    return value === undefined ? undefinedTag : nullTag;
	  }
	  return (symToStringTag && symToStringTag in Object(value))
	    ? getRawTag(value)
	    : objectToString(value);
	}
	
	module.exports = baseGetTag;


/***/ },
/* 34 */
/***/ function(module, exports, __webpack_require__) {

	var root = __webpack_require__(35);
	
	/** Built-in value references. */
	var Symbol = root.Symbol;
	
	module.exports = Symbol;


/***/ },
/* 35 */
/***/ function(module, exports, __webpack_require__) {

	var freeGlobal = __webpack_require__(36);
	
	/** Detect free variable `self`. */
	var freeSelf = typeof self == 'object' && self && self.Object === Object && self;
	
	/** Used as a reference to the global object. */
	var root = freeGlobal || freeSelf || Function('return this')();
	
	module.exports = root;


/***/ },
/* 36 */
/***/ function(module, exports) {

	/* WEBPACK VAR INJECTION */(function(global) {/** Detect free variable `global` from Node.js. */
	var freeGlobal = typeof global == 'object' && global && global.Object === Object && global;
	
	module.exports = freeGlobal;
	
	/* WEBPACK VAR INJECTION */}.call(exports, (function() { return this; }())))

/***/ },
/* 37 */
/***/ function(module, exports, __webpack_require__) {

	var Symbol = __webpack_require__(34);
	
	/** Used for built-in method references. */
	var objectProto = Object.prototype;
	
	/** Used to check objects for own properties. */
	var hasOwnProperty = objectProto.hasOwnProperty;
	
	/**
	 * Used to resolve the
	 * [`toStringTag`](http://ecma-international.org/ecma-262/7.0/#sec-object.prototype.tostring)
	 * of values.
	 */
	var nativeObjectToString = objectProto.toString;
	
	/** Built-in value references. */
	var symToStringTag = Symbol ? Symbol.toStringTag : undefined;
	
	/**
	 * A specialized version of `baseGetTag` which ignores `Symbol.toStringTag` values.
	 *
	 * @private
	 * @param {*} value The value to query.
	 * @returns {string} Returns the raw `toStringTag`.
	 */
	function getRawTag(value) {
	  var isOwn = hasOwnProperty.call(value, symToStringTag),
	      tag = value[symToStringTag];
	
	  try {
	    value[symToStringTag] = undefined;
	    var unmasked = true;
	  } catch (e) {}
	
	  var result = nativeObjectToString.call(value);
	  if (unmasked) {
	    if (isOwn) {
	      value[symToStringTag] = tag;
	    } else {
	      delete value[symToStringTag];
	    }
	  }
	  return result;
	}
	
	module.exports = getRawTag;


/***/ },
/* 38 */
/***/ function(module, exports) {

	/** Used for built-in method references. */
	var objectProto = Object.prototype;
	
	/**
	 * Used to resolve the
	 * [`toStringTag`](http://ecma-international.org/ecma-262/7.0/#sec-object.prototype.tostring)
	 * of values.
	 */
	var nativeObjectToString = objectProto.toString;
	
	/**
	 * Converts `value` to a string using `Object.prototype.toString`.
	 *
	 * @private
	 * @param {*} value The value to convert.
	 * @returns {string} Returns the converted string.
	 */
	function objectToString(value) {
	  return nativeObjectToString.call(value);
	}
	
	module.exports = objectToString;


/***/ },
/* 39 */
/***/ function(module, exports, __webpack_require__) {

	var overArg = __webpack_require__(40);
	
	/** Built-in value references. */
	var getPrototype = overArg(Object.getPrototypeOf, Object);
	
	module.exports = getPrototype;


/***/ },
/* 40 */
/***/ function(module, exports) {

	/**
	 * Creates a unary function that invokes `func` with its argument transformed.
	 *
	 * @private
	 * @param {Function} func The function to wrap.
	 * @param {Function} transform The argument transform.
	 * @returns {Function} Returns the new function.
	 */
	function overArg(func, transform) {
	  return function(arg) {
	    return func(transform(arg));
	  };
	}
	
	module.exports = overArg;


/***/ },
/* 41 */
/***/ function(module, exports) {

	/**
	 * Checks if `value` is object-like. A value is object-like if it's not `null`
	 * and has a `typeof` result of "object".
	 *
	 * @static
	 * @memberOf _
	 * @since 4.0.0
	 * @category Lang
	 * @param {*} value The value to check.
	 * @returns {boolean} Returns `true` if `value` is object-like, else `false`.
	 * @example
	 *
	 * _.isObjectLike({});
	 * // => true
	 *
	 * _.isObjectLike([1, 2, 3]);
	 * // => true
	 *
	 * _.isObjectLike(_.noop);
	 * // => false
	 *
	 * _.isObjectLike(null);
	 * // => false
	 */
	function isObjectLike(value) {
	  return value != null && typeof value == 'object';
	}
	
	module.exports = isObjectLike;


/***/ },
/* 42 */
/***/ function(module, exports) {

	/**
	 * Copyright 2015, Yahoo! Inc.
	 * Copyrights licensed under the New BSD License. See the accompanying LICENSE file for terms.
	 */
	'use strict';
	
	var REACT_STATICS = {
	    childContextTypes: true,
	    contextTypes: true,
	    defaultProps: true,
	    displayName: true,
	    getDefaultProps: true,
	    mixins: true,
	    propTypes: true,
	    type: true
	};
	
	var KNOWN_STATICS = {
	    name: true,
	    length: true,
	    prototype: true,
	    caller: true,
	    arguments: true,
	    arity: true
	};
	
	var isGetOwnPropertySymbolsAvailable = typeof Object.getOwnPropertySymbols === 'function';
	
	module.exports = function hoistNonReactStatics(targetComponent, sourceComponent, customStatics) {
	    if (typeof sourceComponent !== 'string') { // don't hoist over string (html) components
	        var keys = Object.getOwnPropertyNames(sourceComponent);
	
	        /* istanbul ignore else */
	        if (isGetOwnPropertySymbolsAvailable) {
	            keys = keys.concat(Object.getOwnPropertySymbols(sourceComponent));
	        }
	
	        for (var i = 0; i < keys.length; ++i) {
	            if (!REACT_STATICS[keys[i]] && !KNOWN_STATICS[keys[i]] && (!customStatics || !customStatics[keys[i]])) {
	                try {
	                    targetComponent[keys[i]] = sourceComponent[keys[i]];
	                } catch (error) {
	
	                }
	            }
	        }
	    }
	
	    return targetComponent;
	};


/***/ },
/* 43 */
/***/ function(module, exports, __webpack_require__) {

	/* WEBPACK VAR INJECTION */(function(process) {/**
	 * Copyright 2013-2015, Facebook, Inc.
	 * All rights reserved.
	 *
	 * This source code is licensed under the BSD-style license found in the
	 * LICENSE file in the root directory of this source tree. An additional grant
	 * of patent rights can be found in the PATENTS file in the same directory.
	 */
	
	'use strict';
	
	/**
	 * Use invariant() to assert state which your program assumes to be true.
	 *
	 * Provide sprintf-style format (only %s is supported) and arguments
	 * to provide information about what broke and what you were
	 * expecting.
	 *
	 * The invariant message will be stripped in production, but the invariant
	 * will remain to ensure logic does not differ in production.
	 */
	
	var invariant = function(condition, format, a, b, c, d, e, f) {
	  if (process.env.NODE_ENV !== 'production') {
	    if (format === undefined) {
	      throw new Error('invariant requires an error message argument');
	    }
	  }
	
	  if (!condition) {
	    var error;
	    if (format === undefined) {
	      error = new Error(
	        'Minified exception occurred; use the non-minified dev environment ' +
	        'for the full error message and additional helpful warnings.'
	      );
	    } else {
	      var args = [a, b, c, d, e, f];
	      var argIndex = 0;
	      error = new Error(
	        format.replace(/%s/g, function() { return args[argIndex++]; })
	      );
	      error.name = 'Invariant Violation';
	    }
	
	    error.framesToPop = 1; // we don't care about invariant's own frame
	    throw error;
	  }
	};
	
	module.exports = invariant;
	
	/* WEBPACK VAR INJECTION */}.call(exports, __webpack_require__(5)))

/***/ },
/* 44 */
/***/ function(module, exports) {

	'use strict';
	
	exports.__esModule = true;
	function createThunkMiddleware(extraArgument) {
	  return function (_ref) {
	    var dispatch = _ref.dispatch,
	        getState = _ref.getState;
	    return function (next) {
	      return function (action) {
	        if (typeof action === 'function') {
	          return action(dispatch, getState, extraArgument);
	        }
	
	        return next(action);
	      };
	    };
	  };
	}
	
	var thunk = createThunkMiddleware();
	thunk.withExtraArgument = createThunkMiddleware;
	
	exports['default'] = thunk;

/***/ },
/* 45 */
/***/ function(module, exports, __webpack_require__) {

	"use strict";
	var redux_1 = __webpack_require__(11);
	var userFilesReducer_1 = __webpack_require__(46);
	var modalReducer_1 = __webpack_require__(49);
	var userNotesReducer_1 = __webpack_require__(50);
	var userAddressReducer_1 = __webpack_require__(51);
	var canUseCloudReducer_1 = __webpack_require__(52);
	var canUseCloudModalReducer_1 = __webpack_require__(53);
	var rootReducer = redux_1.combineReducers({
	    files: userFilesReducer_1.user_files,
	    upload_progress: userFilesReducer_1.upload_progress,
	    modals: modalReducer_1.default,
	    notes: userNotesReducer_1.user_notes,
	    new_notes: userNotesReducer_1.new_user_notes,
	    addresses: userAddressReducer_1.user_addresses,
	    issuer_id: function (state, action) {
	        if (state === void 0) { state = null; }
	        return (state);
	    },
	    file_size_limit: userFilesReducer_1.file_size_limit,
	    can_use_cloud: canUseCloudReducer_1.can_use_cloud,
	    can_use_cloud_modals: canUseCloudModalReducer_1.default
	});
	Object.defineProperty(exports, "__esModule", { value: true });
	exports.default = rootReducer;


/***/ },
/* 46 */
/***/ function(module, exports, __webpack_require__) {

	"use strict";
	var Constants_1 = __webpack_require__(47);
	var Utilites_1 = __webpack_require__(48);
	exports.user_files = function (state, action) {
	    if (state === void 0) { state = []; }
	    var file_index;
	    var new_file;
	    switch (action.type) {
	        case Constants_1.RECEIVE_FILES:
	            return Utilites_1.DeepCopy(action).files;
	        case Constants_1.RECEIVE_UPLOAD_FILES:
	            state.push(Utilites_1.DeepCopy(action).file);
	            return state.slice();
	        case Constants_1.RECEIVE_DELETE_FILE:
	            new_file = Utilites_1.DeepCopy(action).file;
	            file_index = state.indexOf(state.filter(function (p) { return p.id === new_file.id; })[0]);
	            state.splice(file_index, 1);
	            return state.slice();
	        default:
	            return state;
	    }
	};
	exports.upload_progress = function (state, action) {
	    if (state === void 0) { state = null; }
	    switch (action.type) {
	        case Constants_1.REQUEST_UPLOAD_FILE:
	        case Constants_1.REQUEST_UPLOAD_INCREASE:
	        case Constants_1.RECEIVE_UPLOAD_FILES:
	            return action.load_status;
	        default:
	            return state;
	    }
	};
	exports.file_size_limit = function (state, action) {
	    if (state === void 0) { state = 0; }
	    switch (action.type) {
	        case Constants_1.RECEIVE_FILE_SIZE_LIMIT:
	            return action.file_size_limit;
	        default:
	            return state;
	    }
	};


/***/ },
/* 47 */
/***/ function(module, exports) {

	"use strict";
	exports.REQUEST_FILES = 'REQUEST_FILES';
	exports.RECEIVE_FILES = 'RECEIVE_FILES';
	exports.REQUEST_UPLOAD_FILE = 'REQUEST_UPLOAD_FILE';
	exports.RECEIVE_UPLOAD_FILES = 'RECEIVE_UPLOAD_FILES';
	exports.REQUEST_UPLOAD_INCREASE = 'REQUEST_UPLOAD_INCREASE';
	exports.REQUEST_DELETE_FILE = 'REQUEST_DELETE_FILE';
	exports.RECEIVE_DELETE_FILE = 'RECEIVE_DELETE_FILE';
	exports.REQUEST_FILE_SIZE_LIMIT = 'REQUEST_FILE_SIZE_LIMIT';
	exports.RECEIVE_FILE_SIZE_LIMIT = 'RECEIVE_FILE_SIZE_LIMIT';
	exports.REQUEST_CAN_USE_CLOUD = 'REQUEST_CAN_USE_CLOUD';
	exports.RECEIVE_CAN_USE_CLOUD = 'RECEIVE_CAN_USE_CLOUD';
	//export const CONFIRM_CAN_USE_CLOUD='CONFIRM_CAN_USE_CLOUD'
	exports.SHOW_CAN_USE_CLOUD_MODAL = 'SHOW_CAN_USE_CLOUD_MODAL';
	exports.HIDE_CAN_USE_CLOUD_MODAL = 'HIDE_CAN_USE_CLOUD_MODAL';
	exports.SHOW_MODAL = 'SHOW_MODAL';
	exports.SHOW_CONFIRM_MODAL = 'SHOW_CONFIRM_MODAL';
	exports.HIDE_MODAL = 'HIDE_MODAL';
	exports.REQUEST_NOTES = 'REQUEST_NOTES';
	exports.RECEIVE_NOTES = 'RECEIVE_NOTES';
	exports.REQUEST_UPDATE_NOTE = 'REQUEST_UPDATE_NOTE';
	exports.RECEIVE_UPDATE_NOTE = 'RECEIVE_UPDATE_NOTE';
	exports.REQUEST_ADD_NOTE = 'REQUEST_ADD_NOTE';
	exports.RECEIVE_ADD_NOTE = 'RECEIVE_ADD_NOTE';
	exports.REQUEST_DELETE_NOTE = 'REQUEST_DELETE_NOTE';
	exports.RECEIVE_DELETE_NOTE = 'RECEIVE_DELETE_NOTE';
	exports.ADD_NEW_NOTE = 'ADD_NEW_NOTE';
	exports.DELETE_NEW_NOTE = 'DELETE_NEW_NOTE';
	exports.REQUEST_ADDRESSES = 'REQUEST_ADDRESSES';
	exports.RECEIVE_ADDRESSES = 'RECEIVE_ADDRESSES';
	exports.REQUEST_UPDATE_ADDRESS = 'REQUEST_UPDATE_ADDRESS';
	exports.RECEIVE_UPDATE_ADDRESS = 'RECEIVE_UPDATE_ADDRESS';
	//export const REQUEST_ADD_ADDRESS='REQUEST_ADD_ADDRESS'
	//export const RECEIVE_ADD_ADDRESS='RECEIVE_ADD_ADDRESS'
	exports.REQUEST_DELETE_ADDRESS = 'REQUEST_DELETE_ADDRESS';
	exports.RECEIVE_DELETE_ADDRESS = 'RECEIVE_DELETE_ADDRESS';
	//export const ADD_NEW_ADDRESS = 'ADD_NEW_ADDRESS' 
	//export const DELETE_NEW_ADDRESS = 'DELETE_NEW_ADDRESS' 


/***/ },
/* 48 */
/***/ function(module, exports) {

	"use strict";
	function GetStyleExtention(extention) {
	    var default_style_extention = "icon-doc doc";
	    if (extention === null || extention === undefined) {
	        return default_style_extention;
	    }
	    switch (extention.toLowerCase()) {
	        case "jpg":
	        case "bmp":
	        case "tif":
	        case "png":
	            return "icon-file-image image";
	        case "xml":
	            return "icon-file-code code";
	        case "doc":
	        case "docx":
	        case "rtf":
	            return "icon-file-word word";
	        case "pdf":
	            return "icon-file-pdf pdf";
	        case "xls":
	        case "xlsx":
	            return "icon-file-excel excel";
	        case "zip":
	        case "rar":
	            return "icon-file-archive archive";
	        default:
	            return default_style_extention;
	    }
	}
	exports.GetStyleExtention = GetStyleExtention;
	function GenerateGUID() {
	    var d = new Date().getTime();
	    if (window.performance && typeof window.performance.now === "function") {
	        d += performance.now(); //use high-precision timer if available
	    }
	    var uuid = 'xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx'.replace(/[xy]/g, function (c) {
	        var r = (d + Math.random() * 16) % 16 | 0;
	        d = Math.floor(d / 16);
	        return (c == 'x' ? r : (r & 0x3 | 0x8)).toString(16);
	    });
	    return uuid;
	}
	exports.GenerateGUID = GenerateGUID;
	/*
	export function GetDate() {
	    var today = new Date();
	    var dd = String(today.getDate());
	    var mm = String(today.getMonth()+1);
	    var yyyy = String(today.getFullYear());
	
	    if(Number(dd)<10) {
	        dd='0'+dd
	    }
	
	    if(Number(mm)<10) {
	        mm='0'+mm
	    }
	
	    return dd+'.'+mm+'.'+yyyy;
	}*/
	function DeepCopy(original) {
	    return JSON.parse(JSON.stringify(original));
	}
	exports.DeepCopy = DeepCopy;
	function FormatBytes(bytes) {
	    if (bytes < 1024)
	        return bytes + " Bytes";
	    else if (bytes < 1048576)
	        return Number((bytes / 1024).toFixed(3)).toString().replace(".", ",") + " KB";
	    else if (bytes < 1073741824)
	        return Number((bytes / 1048576).toFixed(3)).toString().replace(".", ",") + " MB";
	    else
	        return Number((bytes / 1073741824).toFixed(3)).toString().replace(".", ",") + " GB";
	}
	exports.FormatBytes = FormatBytes;
	;


/***/ },
/* 49 */
/***/ function(module, exports, __webpack_require__) {

	"use strict";
	var redux_1 = __webpack_require__(11);
	var Constants_1 = __webpack_require__(47);
	var is_showing = function (state, action) {
	    if (state === void 0) { state = false; }
	    switch (action.type) {
	        case Constants_1.SHOW_CONFIRM_MODAL:
	        case Constants_1.SHOW_MODAL:
	            return state = true;
	        case Constants_1.HIDE_MODAL:
	            return state = false;
	        default:
	            return state;
	    }
	};
	var body = function (state, action) {
	    if (state === void 0) { state = ''; }
	    switch (action.type) {
	        case Constants_1.SHOW_CONFIRM_MODAL:
	        case Constants_1.SHOW_MODAL:
	            return state = action.body;
	        default:
	            return state;
	    }
	};
	var action = function (state, action) {
	    if (state === void 0) { state = null; }
	    switch (action.type) {
	        case Constants_1.SHOW_CONFIRM_MODAL:
	            return state = action.action;
	        case Constants_1.SHOW_MODAL:
	            return state = null;
	        default:
	            return state;
	    }
	};
	Object.defineProperty(exports, "__esModule", { value: true });
	exports.default = redux_1.combineReducers({
	    isShowing: is_showing,
	    body: body,
	    action: action
	});


/***/ },
/* 50 */
/***/ function(module, exports, __webpack_require__) {

	"use strict";
	var Constants_1 = __webpack_require__(47);
	var Utilites_1 = __webpack_require__(48);
	exports.user_notes = function (state, action) {
	    if (state === void 0) { state = []; }
	    var note_index;
	    var new_note;
	    var exist_note;
	    switch (action.type) {
	        case Constants_1.RECEIVE_NOTES:
	            return Utilites_1.DeepCopy(action).notes;
	        case Constants_1.RECEIVE_ADD_NOTE:
	            state.push(Utilites_1.DeepCopy(action).note);
	            return state.slice();
	        case Constants_1.RECEIVE_DELETE_NOTE:
	            new_note = Utilites_1.DeepCopy(action).note;
	            note_index = state.indexOf(state.filter(function (p) { return p.id === new_note.id; })[0]);
	            state.splice(note_index, 1);
	            return state.slice();
	        case Constants_1.RECEIVE_UPDATE_NOTE:
	            new_note = Utilites_1.DeepCopy(action).note;
	            note_index = state.indexOf(state.filter(function (p) { return p.id === new_note.id; })[0]);
	            state[note_index] = new_note;
	            return state.slice();
	        default:
	            return state;
	    }
	};
	exports.new_user_notes = function (state, action) {
	    if (state === void 0) { state = []; }
	    switch (action.type) {
	        case Constants_1.ADD_NEW_NOTE:
	            state.push(Utilites_1.DeepCopy(action).note);
	            return state.slice();
	        case Constants_1.DELETE_NEW_NOTE:
	            var note_index = state.indexOf(state.filter(function (p) { return p.id === action.id; })[0]);
	            state.splice(note_index, 1);
	            return state.slice();
	        default:
	            return state;
	    }
	};


/***/ },
/* 51 */
/***/ function(module, exports, __webpack_require__) {

	"use strict";
	var Constants_1 = __webpack_require__(47);
	var Utilites_1 = __webpack_require__(48);
	exports.user_addresses = function (state, action) {
	    if (state === void 0) { state = []; }
	    var address_index;
	    var new_adress;
	    switch (action.type) {
	        case Constants_1.RECEIVE_ADDRESSES:
	            return Utilites_1.DeepCopy(action).addresses;
	        case Constants_1.RECEIVE_DELETE_ADDRESS:
	            new_adress = Utilites_1.DeepCopy(action).address;
	            address_index = state.indexOf(state.filter(function (p) { return p.id === new_adress.id; })[0]);
	            state.splice(address_index, 1);
	            return state.slice();
	        case Constants_1.RECEIVE_UPDATE_ADDRESS:
	            new_adress = Utilites_1.DeepCopy(action).address;
	            address_index = state.indexOf(state.filter(function (p) { return p.id === new_adress.id; })[0]);
	            if (address_index == -1) {
	                state.push(Utilites_1.DeepCopy(action).address);
	            }
	            else {
	                state[address_index] = new_adress;
	            }
	            return state.slice();
	        default:
	            return state;
	    }
	};


/***/ },
/* 52 */
/***/ function(module, exports, __webpack_require__) {

	"use strict";
	var Constants_1 = __webpack_require__(47);
	exports.can_use_cloud = function (state, action) {
	    if (state === void 0) { state = false; }
	    switch (action.type) {
	        case Constants_1.RECEIVE_CAN_USE_CLOUD:
	            return state = action.can_use_cloud;
	        default:
	            return state;
	    }
	};


/***/ },
/* 53 */
/***/ function(module, exports, __webpack_require__) {

	"use strict";
	var redux_1 = __webpack_require__(11);
	var Constants_1 = __webpack_require__(47);
	var isShowing = function (state, action) {
	    if (state === void 0) { state = false; }
	    switch (action.type) {
	        case Constants_1.SHOW_CAN_USE_CLOUD_MODAL:
	            return state = true;
	        case Constants_1.HIDE_CAN_USE_CLOUD_MODAL:
	            return state = false;
	        default:
	            return state;
	    }
	};
	var action = function (state, action) {
	    if (state === void 0) { state = null; }
	    switch (action.type) {
	        case Constants_1.SHOW_CAN_USE_CLOUD_MODAL:
	            return state = action.action;
	        default:
	            return state;
	    }
	};
	Object.defineProperty(exports, "__esModule", { value: true });
	exports.default = redux_1.combineReducers({
	    isShowing: isShowing,
	    action: action
	});


/***/ },
/* 54 */
/***/ function(module, exports, __webpack_require__) {

	"use strict";
	var react_redux_1 = __webpack_require__(3);
	var IssuerUserFilesView_1 = __webpack_require__(55);
	var IssuerFileActionCreators_1 = __webpack_require__(57);
	var ModalActionCreators_1 = __webpack_require__(59);
	var AccessCloudActionCreators_1 = __webpack_require__(60);
	var mapStateToProps = function (state) { return ({
	    files: state.files,
	    upload_progress: state.upload_progress,
	    issuer_id: state.issuer_id,
	    file_size_limit: state.file_size_limit,
	    can_use_cloud: state.can_use_cloud
	}); };
	var mapDispatchToProps = function (dispatch) { return ({
	    getFiles: function (issuer_id) { return (dispatch(IssuerFileActionCreators_1.IssuerFileActionCreators.GetFiles(issuer_id))); },
	    uploadFile: function (file, issuer_id, can_use_cloud) {
	        if (can_use_cloud) {
	            dispatch(IssuerFileActionCreators_1.IssuerFileActionCreators.UploadFile(file, issuer_id));
	        }
	        else {
	            dispatch(AccessCloudActionCreators_1.AccessCloudActionCreators.showModal(IssuerFileActionCreators_1.IssuerFileActionCreators.UploadFile(file, issuer_id)));
	        }
	    },
	    deleteFile: function (file_id, file_name) {
	        dispatch(ModalActionCreators_1.ModalActionCreators.showConfirmModal("Вы действительно хотите удалить файл: " + file_name + "?", IssuerFileActionCreators_1.IssuerFileActionCreators.DeleteFile(file_id)));
	    },
	    getFileSizeLimit: function () { return (dispatch(IssuerFileActionCreators_1.IssuerFileActionCreators.GetFileSizeLimit())); },
	    showErrorMessage: function (error_message) { return (dispatch(ModalActionCreators_1.ModalActionCreators.showModal(error_message))); }
	}); };
	Object.defineProperty(exports, "__esModule", { value: true });
	exports.default = react_redux_1.connect(mapStateToProps, mapDispatchToProps)(IssuerUserFilesView_1.default);


/***/ },
/* 55 */
/***/ function(module, exports, __webpack_require__) {

	"use strict";
	var __extends = (this && this.__extends) || function (d, b) {
	    for (var p in b) if (b.hasOwnProperty(p)) d[p] = b[p];
	    function __() { this.constructor = d; }
	    d.prototype = b === null ? Object.create(b) : (__.prototype = b.prototype, new __());
	};
	var React = __webpack_require__(1);
	var Utilites_1 = __webpack_require__(48);
	var ProgressBar_1 = __webpack_require__(56);
	;
	;
	var IssuerUserFiles = (function (_super) {
	    __extends(IssuerUserFiles, _super);
	    function IssuerUserFiles() {
	        return _super.apply(this, arguments) || this;
	    }
	    IssuerUserFiles.prototype.componentDidMount = function () {
	        this.props.getFiles(this.props.issuer_id);
	        this.props.getFileSizeLimit();
	    };
	    IssuerUserFiles.prototype.loadfile = function (event) {
	        //console.log(event.target.files[0]);
	        var file = event.target.files[0];
	        if (this.getFileSizes() + file.size > this.props.file_size_limit) {
	            this.props.showErrorMessage("Превышен общий размер разрешенной загрузки: " + Utilites_1.FormatBytes(this.props.file_size_limit));
	        }
	        else {
	            this.props.uploadFile(file, this.props.issuer_id, this.props.can_use_cloud);
	        }
	    };
	    IssuerUserFiles.prototype.getFileSizes = function () {
	        var already_have = 0;
	        for (var i = 0; i < this.props.files.length; i++) {
	            already_have += this.props.files[i].file_size;
	        }
	        return already_have;
	    };
	    IssuerUserFiles.prototype.CutFileName = function (file_name) {
	        if (file_name.length < 25) {
	            return React.createElement("span", { className: "file_name" }, file_name);
	        }
	        return React.createElement("span", { className: "file_name title" },
	            file_name.substring(0, 25),
	            React.createElement("em", null, file_name),
	            React.createElement("span", { className: "file_name_fader" }));
	    };
	    IssuerUserFiles.prototype.render = function () {
	        var _this = this;
	        var files = this.props.files.map(function (file) {
	            var extention = file.file_name.substr(file.file_name.lastIndexOf('.') + 1, file.file_name.length);
	            return (React.createElement("tr", { key: file.id, className: "file_item" },
	                React.createElement("td", null,
	                    React.createElement("div", null,
	                        React.createElement("a", { href: "/UserFile/Load/" + file.id },
	                            React.createElement("span", { className: Utilites_1.GetStyleExtention(extention) + " ico" }),
	                            _this.CutFileName(file.file_name)),
	                        React.createElement("span", { className: "explain" },
	                            "  (",
	                            Utilites_1.FormatBytes(file.file_size),
	                            ")"),
	                        React.createElement("span", { className: "ddel", onClick: _this.props.deleteFile.bind(null, file.id, file.file_name) },
	                            React.createElement("span", { className: "ddel_1" },
	                                React.createElement("b", null, "x"),
	                                "\u0423\u0434\u0430\u043B\u0438\u0442\u044C")),
	                        React.createElement("div", { className: "link_block_info" },
	                            React.createElement("span", { className: "explain" },
	                                "\u0414\u0430\u0442\u0430 \u0437\u0430\u0433\u0440\u0443\u0437\u043A\u0438:",
	                                file.update_date))))));
	        });
	        var upload_style = this.props.upload_progress == null ? {} : { display: "none" };
	        return (React.createElement("div", { className: "link_block", id: "lb_1" },
	            React.createElement("h3", { className: "cloud_switcher_header" },
	                React.createElement("span", { className: "icon-docs" }),
	                "\u0424\u0430\u0439\u043B\u044B",
	                React.createElement("span", { className: "icon-angle-down cloud_switcher_ar" })),
	            React.createElement("table", { className: "extra_table", style: { border: 0 }, cellPadding: "0", cellSpacing: "0" },
	                React.createElement("tbody", null,
	                    React.createElement("tr", null,
	                        React.createElement("td", null,
	                            React.createElement("div", { className: "icon_block large", style: upload_style },
	                                React.createElement("span", { className: "icon-upload new ico" }),
	                                React.createElement("span", null, "\u0417\u0430\u0433\u0440\u0443\u0437\u0438\u0442\u044C"),
	                                React.createElement("form", null,
	                                    React.createElement("input", { type: "file", onChange: this.loadfile.bind(this), name: "cloud_file_input" }))),
	                            React.createElement(ProgressBar_1.default, { upload_progress: this.props.upload_progress }))),
	                    files))));
	    };
	    return IssuerUserFiles;
	}(React.Component));
	Object.defineProperty(exports, "__esModule", { value: true });
	exports.default = IssuerUserFiles;


/***/ },
/* 56 */
/***/ function(module, exports, __webpack_require__) {

	"use strict";
	var __extends = (this && this.__extends) || function (d, b) {
	    for (var p in b) if (b.hasOwnProperty(p)) d[p] = b[p];
	    function __() { this.constructor = d; }
	    d.prototype = b === null ? Object.create(b) : (__.prototype = b.prototype, new __());
	};
	var React = __webpack_require__(1);
	;
	;
	var ProgressBar = (function (_super) {
	    __extends(ProgressBar, _super);
	    function ProgressBar() {
	        return _super.apply(this, arguments) || this;
	    }
	    ProgressBar.prototype.render = function () {
	        if (this.props.upload_progress === null) {
	            return (React.createElement("span", null));
	        }
	        var shown_progress = this.props.upload_progress == null ? "" : this.props.upload_progress + "%";
	        return (React.createElement("div", { className: "progress" },
	            React.createElement("div", { className: "progress-bar", style: { width: this.props.upload_progress + "%" } }, this.props.upload_progress + "%")));
	    };
	    return ProgressBar;
	}(React.Component));
	Object.defineProperty(exports, "__esModule", { value: true });
	exports.default = ProgressBar;


/***/ },
/* 57 */
/***/ function(module, exports, __webpack_require__) {

	"use strict";
	var __awaiter = (this && this.__awaiter) || function (thisArg, _arguments, P, generator) {
	    return new (P || (P = Promise))(function (resolve, reject) {
	        function fulfilled(value) { try { step(generator.next(value)); } catch (e) { reject(e); } }
	        function rejected(value) { try { step(generator["throw"](value)); } catch (e) { reject(e); } }
	        function step(result) { result.done ? resolve(result.value) : new P(function (resolve) { resolve(result.value); }).then(fulfilled, rejected); }
	        step((generator = generator.apply(thisArg, _arguments)).next());
	    });
	};
	var __generator = (this && this.__generator) || function (thisArg, body) {
	    var _ = { label: 0, sent: function() { if (t[0] & 1) throw t[1]; return t[1]; }, trys: [], ops: [] }, f, y, t;
	    return { next: verb(0), "throw": verb(1), "return": verb(2) };
	    function verb(n) { return function (v) { return step([n, v]); }; }
	    function step(op) {
	        if (f) throw new TypeError("Generator is already executing.");
	        while (_) try {
	            if (f = 1, y && (t = y[op[0] & 2 ? "return" : op[0] ? "throw" : "next"]) && !(t = t.call(y, op[1])).done) return t;
	            if (y = 0, t) op = [0, t.value];
	            switch (op[0]) {
	                case 0: case 1: t = op; break;
	                case 4: _.label++; return { value: op[1], done: false };
	                case 5: _.label++; y = op[1]; op = [0]; continue;
	                case 7: op = _.ops.pop(); _.trys.pop(); continue;
	                default:
	                    if (!(t = _.trys, t = t.length > 0 && t[t.length - 1]) && (op[0] === 6 || op[0] === 2)) { _ = 0; continue; }
	                    if (op[0] === 3 && (!t || (op[1] > t[0] && op[1] < t[3]))) { _.label = op[1]; break; }
	                    if (op[0] === 6 && _.label < t[1]) { _.label = t[1]; t = op; break; }
	                    if (t && _.label < t[2]) { _.label = t[2]; _.ops.push(op); break; }
	                    if (t[2]) _.ops.pop();
	                    _.trys.pop(); continue;
	            }
	            op = body.call(thisArg, _);
	        } catch (e) { op = [6, e]; y = 0; } finally { f = t = 0; }
	        if (op[0] & 5) throw op[1]; return { value: op[0] ? op[1] : void 0, done: true };
	    }
	};
	var Constants_1 = __webpack_require__(47);
	var IssuerUserFilesAPI_1 = __webpack_require__(58);
	var ModalActionCreators_1 = __webpack_require__(59);
	var IssuerFileActionCreators = (function () {
	    function IssuerFileActionCreators() {
	    }
	    IssuerFileActionCreators._requestFiles = function () {
	        return { type: Constants_1.REQUEST_FILES };
	    };
	    IssuerFileActionCreators._recieveFiles = function (files) {
	        return { type: Constants_1.RECEIVE_FILES, files: files };
	    };
	    IssuerFileActionCreators.GetFiles = function (issuer_id) {
	        var _this = this;
	        return function (dispatch) { return __awaiter(_this, void 0, void 0, function () {
	            var files, error_1;
	            return __generator(this, function (_a) {
	                switch (_a.label) {
	                    case 0:
	                        _a.trys.push([0, 2, , 3]);
	                        dispatch(this._requestFiles());
	                        return [4 /*yield*/, IssuerUserFilesAPI_1.default.Get(issuer_id)];
	                    case 1:
	                        files = _a.sent();
	                        dispatch(this._recieveFiles(files));
	                        return [3 /*break*/, 3];
	                    case 2:
	                        error_1 = _a.sent();
	                        dispatch(ModalActionCreators_1.ModalActionCreators.showModal("Ошибка загрузки файлов: " + (error_1.message || error_1)));
	                        return [3 /*break*/, 3];
	                    case 3: return [2 /*return*/];
	                }
	            });
	        }); };
	    };
	    IssuerFileActionCreators._requestFileSizeLimit = function () {
	        return { type: Constants_1.REQUEST_FILE_SIZE_LIMIT };
	    };
	    IssuerFileActionCreators._recieveFileSizeLimit = function (file_size_limit) {
	        return { type: Constants_1.RECEIVE_FILE_SIZE_LIMIT, file_size_limit: file_size_limit };
	    };
	    IssuerFileActionCreators.GetFileSizeLimit = function () {
	        var _this = this;
	        return function (dispatch) { return __awaiter(_this, void 0, void 0, function () {
	            var file_size_limit, error_2;
	            return __generator(this, function (_a) {
	                switch (_a.label) {
	                    case 0:
	                        _a.trys.push([0, 2, , 3]);
	                        dispatch(this._requestFileSizeLimit());
	                        return [4 /*yield*/, IssuerUserFilesAPI_1.default.GetFileSizeLimit()];
	                    case 1:
	                        file_size_limit = _a.sent();
	                        dispatch(this._recieveFileSizeLimit(file_size_limit));
	                        return [3 /*break*/, 3];
	                    case 2:
	                        error_2 = _a.sent();
	                        dispatch(ModalActionCreators_1.ModalActionCreators.showModal("Ошибка загрузки файлов: " + (error_2.message || error_2)));
	                        return [3 /*break*/, 3];
	                    case 3: return [2 /*return*/];
	                }
	            });
	        }); };
	    };
	    IssuerFileActionCreators._requestUploadFile = function () {
	        return { type: Constants_1.REQUEST_UPLOAD_FILE, load_status: 0 };
	    };
	    IssuerFileActionCreators._requestUploadIncrease = function (notification) {
	        var status = parseFloat(notification);
	        return { type: Constants_1.REQUEST_UPLOAD_INCREASE, load_status: status };
	    };
	    IssuerFileActionCreators._recieveUploadFile = function (file) {
	        return { type: Constants_1.RECEIVE_UPLOAD_FILES, file: file, load_status: null };
	    };
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
	    IssuerFileActionCreators.UploadFile = function (file, issuer_id) {
	        var _this = this;
	        return function (dispatch) { return __awaiter(_this, void 0, void 0, function () {
	            var _this = this;
	            var res_file, error_3;
	            return __generator(this, function (_a) {
	                switch (_a.label) {
	                    case 0:
	                        _a.trys.push([0, 2, , 3]);
	                        dispatch(this._requestUploadFile);
	                        return [4 /*yield*/, IssuerUserFilesAPI_1.default.Upload(file, issuer_id, function (progress) { dispatch(_this._requestUploadIncrease(progress)); })];
	                    case 1:
	                        res_file = _a.sent();
	                        dispatch(this._recieveUploadFile(res_file));
	                        return [3 /*break*/, 3];
	                    case 2:
	                        error_3 = _a.sent();
	                        dispatch(ModalActionCreators_1.ModalActionCreators.showModal("Ошибка загрузки файла: " + (error_3.message || error_3)));
	                        return [3 /*break*/, 3];
	                    case 3: return [2 /*return*/];
	                }
	            });
	        }); };
	    };
	    IssuerFileActionCreators._requestDeleteFile = function () {
	        return { type: Constants_1.REQUEST_DELETE_FILE };
	    };
	    IssuerFileActionCreators._recieveDeleteFile = function (file) {
	        return { type: Constants_1.RECEIVE_DELETE_FILE, file: file };
	    };
	    IssuerFileActionCreators.DeleteFile = function (file_id) {
	        var _this = this;
	        return function (dispatch) { return __awaiter(_this, void 0, void 0, function () {
	            var file, error_4;
	            return __generator(this, function (_a) {
	                switch (_a.label) {
	                    case 0:
	                        _a.trys.push([0, 2, , 3]);
	                        dispatch(this._requestDeleteFile);
	                        return [4 /*yield*/, IssuerUserFilesAPI_1.default.Delete(file_id)];
	                    case 1:
	                        file = _a.sent();
	                        dispatch(this._recieveDeleteFile(file));
	                        return [3 /*break*/, 3];
	                    case 2:
	                        error_4 = _a.sent();
	                        dispatch(ModalActionCreators_1.ModalActionCreators.showModal("Ошибка удаления файла: " + (error_4.message || error_4)));
	                        return [3 /*break*/, 3];
	                    case 3: return [2 /*return*/];
	                }
	            });
	        }); };
	    };
	    return IssuerFileActionCreators;
	}());
	exports.IssuerFileActionCreators = IssuerFileActionCreators;


/***/ },
/* 58 */
/***/ function(module, exports) {

	"use strict";
	var IssuerUserFilesAPI = (function () {
	    function IssuerUserFilesAPI() {
	    }
	    IssuerUserFilesAPI.Get = function (issuer_id) {
	        return new Promise(function (resolve, reject) {
	            var xhr = new XMLHttpRequest();
	            var params = 'issuer_id=' + issuer_id;
	            xhr.open('GET', '/UserFile/Get?' + params, true);
	            xhr.send();
	            xhr.onreadystatechange = function () {
	                if (this.readyState != 4) {
	                    return;
	                }
	                if (this.status != 200) {
	                    reject(new Error('ошибка: ' + (this.status ? this.statusText : 'запрос не удался')));
	                }
	                resolve(JSON.parse(this.responseText));
	            };
	        });
	    };
	    IssuerUserFilesAPI.GetFileSizeLimit = function () {
	        return new Promise(function (resolve, reject) {
	            var xhr = new XMLHttpRequest();
	            xhr.open('GET', '/UserFile/GetFileSizeLimit/', true);
	            xhr.send();
	            xhr.onreadystatechange = function () {
	                if (this.readyState != 4) {
	                    return;
	                }
	                if (this.status != 200) {
	                    reject(new Error('ошибка: ' + (this.status ? this.statusText : 'запрос не удался')));
	                }
	                resolve(JSON.parse(this.responseText));
	            };
	        });
	    };
	    IssuerUserFilesAPI.Upload = function (file, issuer_id, progress_func) {
	        return new Promise(function (resolve, reject) {
	            var formData = new FormData();
	            formData.append("file", file);
	            formData.append("issuer_id", issuer_id);
	            var xhr = new XMLHttpRequest();
	            xhr.upload.onprogress = function (event) {
	                var progres = Math.floor((Number(event.loaded) / Number(event.total)) * 100);
	                progress_func(progres);
	            };
	            xhr.onreadystatechange = function () {
	                if (this.readyState != 4) {
	                    return;
	                }
	                if (this.status != 200) {
	                    reject(new Error('ошибка: ' + (this.status ? this.statusText : 'запрос не удался')));
	                }
	                resolve(JSON.parse(this.responseText));
	            };
	            xhr.open("POST", "/UserFile/Upload/", true);
	            xhr.send(formData);
	        });
	    };
	    IssuerUserFilesAPI.Delete = function (file_id) {
	        return new Promise(function (resolve, reject) {
	            var xhr = new XMLHttpRequest();
	            var params = 'file_id=' + file_id;
	            xhr.open('GET', '/UserFile/Delete?' + params, true);
	            xhr.send();
	            xhr.onreadystatechange = function () {
	                if (this.readyState != 4) {
	                    return;
	                }
	                if (this.status != 200) {
	                    reject(new Error('ошибка: ' + (this.status ? this.statusText : 'запрос не удался')));
	                }
	                resolve(JSON.parse(this.responseText));
	            };
	        });
	    };
	    return IssuerUserFilesAPI;
	}());
	Object.defineProperty(exports, "__esModule", { value: true });
	exports.default = IssuerUserFilesAPI;


/***/ },
/* 59 */
/***/ function(module, exports, __webpack_require__) {

	"use strict";
	var Constants_1 = __webpack_require__(47);
	var ModalActionCreators = (function () {
	    function ModalActionCreators() {
	    }
	    ModalActionCreators.showConfirmModal = function (body, action) {
	        return {
	            type: Constants_1.SHOW_CONFIRM_MODAL,
	            body: body,
	            action: action
	        };
	    };
	    ModalActionCreators.showModal = function (body) {
	        return {
	            type: Constants_1.SHOW_MODAL,
	            body: body,
	            action: null
	        };
	    };
	    ModalActionCreators.hideModal = function () {
	        return {
	            type: Constants_1.HIDE_MODAL
	        };
	    };
	    return ModalActionCreators;
	}());
	exports.ModalActionCreators = ModalActionCreators;


/***/ },
/* 60 */
/***/ function(module, exports, __webpack_require__) {

	"use strict";
	var __awaiter = (this && this.__awaiter) || function (thisArg, _arguments, P, generator) {
	    return new (P || (P = Promise))(function (resolve, reject) {
	        function fulfilled(value) { try { step(generator.next(value)); } catch (e) { reject(e); } }
	        function rejected(value) { try { step(generator["throw"](value)); } catch (e) { reject(e); } }
	        function step(result) { result.done ? resolve(result.value) : new P(function (resolve) { resolve(result.value); }).then(fulfilled, rejected); }
	        step((generator = generator.apply(thisArg, _arguments)).next());
	    });
	};
	var __generator = (this && this.__generator) || function (thisArg, body) {
	    var _ = { label: 0, sent: function() { if (t[0] & 1) throw t[1]; return t[1]; }, trys: [], ops: [] }, f, y, t;
	    return { next: verb(0), "throw": verb(1), "return": verb(2) };
	    function verb(n) { return function (v) { return step([n, v]); }; }
	    function step(op) {
	        if (f) throw new TypeError("Generator is already executing.");
	        while (_) try {
	            if (f = 1, y && (t = y[op[0] & 2 ? "return" : op[0] ? "throw" : "next"]) && !(t = t.call(y, op[1])).done) return t;
	            if (y = 0, t) op = [0, t.value];
	            switch (op[0]) {
	                case 0: case 1: t = op; break;
	                case 4: _.label++; return { value: op[1], done: false };
	                case 5: _.label++; y = op[1]; op = [0]; continue;
	                case 7: op = _.ops.pop(); _.trys.pop(); continue;
	                default:
	                    if (!(t = _.trys, t = t.length > 0 && t[t.length - 1]) && (op[0] === 6 || op[0] === 2)) { _ = 0; continue; }
	                    if (op[0] === 3 && (!t || (op[1] > t[0] && op[1] < t[3]))) { _.label = op[1]; break; }
	                    if (op[0] === 6 && _.label < t[1]) { _.label = t[1]; t = op; break; }
	                    if (t && _.label < t[2]) { _.label = t[2]; _.ops.push(op); break; }
	                    if (t[2]) _.ops.pop();
	                    _.trys.pop(); continue;
	            }
	            op = body.call(thisArg, _);
	        } catch (e) { op = [6, e]; y = 0; } finally { f = t = 0; }
	        if (op[0] & 5) throw op[1]; return { value: op[0] ? op[1] : void 0, done: true };
	    }
	};
	var Constants_1 = __webpack_require__(47);
	var AccessCloudAPI_1 = __webpack_require__(61);
	var ModalActionCreators_1 = __webpack_require__(59);
	var AccessCloudActionCreators = (function () {
	    function AccessCloudActionCreators() {
	    }
	    AccessCloudActionCreators._requestCanUseCloud = function () {
	        return { type: Constants_1.REQUEST_CAN_USE_CLOUD };
	    };
	    AccessCloudActionCreators._recieveCanUseCloud = function (can_use_cloud) {
	        return { type: Constants_1.RECEIVE_CAN_USE_CLOUD, can_use_cloud: can_use_cloud };
	    };
	    AccessCloudActionCreators.CanUseCloud = function () {
	        var _this = this;
	        return function (dispatch) { return __awaiter(_this, void 0, void 0, function () {
	            var can_use_cloud, error_1;
	            return __generator(this, function (_a) {
	                switch (_a.label) {
	                    case 0:
	                        _a.trys.push([0, 2, , 3]);
	                        dispatch(this._requestCanUseCloud());
	                        return [4 /*yield*/, AccessCloudAPI_1.default.CanUseCloud()];
	                    case 1:
	                        can_use_cloud = _a.sent();
	                        dispatch(this._recieveCanUseCloud(can_use_cloud));
	                        return [3 /*break*/, 3];
	                    case 2:
	                        error_1 = _a.sent();
	                        dispatch(ModalActionCreators_1.ModalActionCreators.showModal("Ошибка запроса доступа к облаку: " + (error_1.message || error_1)));
	                        return [3 /*break*/, 3];
	                    case 3: return [2 /*return*/];
	                }
	            });
	        }); };
	    };
	    AccessCloudActionCreators.ConfirmCanUseCloud = function (action) {
	        var _this = this;
	        return function (dispatch) { return __awaiter(_this, void 0, void 0, function () {
	            var can_use_cloud, error_2;
	            return __generator(this, function (_a) {
	                switch (_a.label) {
	                    case 0:
	                        _a.trys.push([0, 2, , 3]);
	                        return [4 /*yield*/, AccessCloudAPI_1.default.ConfirmUseCloud()];
	                    case 1:
	                        can_use_cloud = _a.sent();
	                        if (can_use_cloud) {
	                            dispatch(action);
	                            dispatch(this._recieveCanUseCloud(true));
	                        }
	                        dispatch(this.hideModal());
	                        return [3 /*break*/, 3];
	                    case 2:
	                        error_2 = _a.sent();
	                        dispatch(this.hideModal());
	                        dispatch(ModalActionCreators_1.ModalActionCreators.showModal("Ошибка запроса доступа к облаку: " + (error_2.message || error_2)));
	                        return [3 /*break*/, 3];
	                    case 3: return [2 /*return*/];
	                }
	            });
	        }); };
	    };
	    AccessCloudActionCreators.hideModal = function () {
	        return {
	            type: Constants_1.HIDE_CAN_USE_CLOUD_MODAL
	        };
	    };
	    AccessCloudActionCreators.showModal = function (action) {
	        return {
	            type: Constants_1.SHOW_CAN_USE_CLOUD_MODAL,
	            action: action
	        };
	    };
	    return AccessCloudActionCreators;
	}());
	exports.AccessCloudActionCreators = AccessCloudActionCreators;


/***/ },
/* 61 */
/***/ function(module, exports) {

	"use strict";
	var AccessCloudAPI = (function () {
	    function AccessCloudAPI() {
	    }
	    AccessCloudAPI.CanUseCloud = function () {
	        return new Promise(function (resolve, reject) {
	            var xhr = new XMLHttpRequest();
	            xhr.open('GET', '/AccessCloud/CheckCloudUsing/', true);
	            xhr.send();
	            xhr.onreadystatechange = function () {
	                if (this.readyState != 4) {
	                    return;
	                }
	                if (this.status != 200) {
	                    reject(new Error('ошибка: ' + (this.status ? this.statusText : 'запрос не удался')));
	                }
	                resolve(JSON.parse(this.responseText));
	            };
	        });
	    };
	    AccessCloudAPI.ConfirmUseCloud = function () {
	        return new Promise(function (resolve, reject) {
	            var xhr = new XMLHttpRequest();
	            xhr.open('GET', '/AccessCloud/ConfirmCloudUsing/', true);
	            xhr.send();
	            xhr.onreadystatechange = function () {
	                if (this.readyState != 4) {
	                    return;
	                }
	                if (this.status != 200) {
	                    reject(new Error('ошибка: ' + (this.status ? this.statusText : 'запрос не удался')));
	                }
	                resolve(JSON.parse(this.responseText));
	            };
	        });
	    };
	    return AccessCloudAPI;
	}());
	Object.defineProperty(exports, "__esModule", { value: true });
	exports.default = AccessCloudAPI;


/***/ },
/* 62 */
/***/ function(module, exports, __webpack_require__) {

	"use strict";
	var react_redux_1 = __webpack_require__(3);
	var IssuerUserNotesView_1 = __webpack_require__(63);
	var IssuerNoteActionCreators_1 = __webpack_require__(65);
	var ModalActionCreators_1 = __webpack_require__(59);
	var AccessCloudActionCreators_1 = __webpack_require__(60);
	var mapStateToProps = function (state) { return ({
	    notes: state.notes,
	    new_notes: state.new_notes,
	    issuer_id: state.issuer_id,
	    can_use_cloud: state.can_use_cloud
	}); };
	var mapDispatchToProps = function (dispatch) { return ({
	    getNotes: function (issuer_id) { return (dispatch(IssuerNoteActionCreators_1.IssuerNoteActionCreators.GetNotes(issuer_id))); },
	    saveNote: function (note_id, content, issuer_id, can_use_cloud) {
	        if (can_use_cloud) {
	            dispatch(IssuerNoteActionCreators_1.IssuerNoteActionCreators.AddNote(note_id, content, issuer_id));
	        }
	        else {
	            dispatch(AccessCloudActionCreators_1.AccessCloudActionCreators.showModal(IssuerNoteActionCreators_1.IssuerNoteActionCreators.AddNote(note_id, content, issuer_id)));
	        }
	    },
	    updadeNote: function (content, issuer_id, note_id) { return (dispatch(IssuerNoteActionCreators_1.IssuerNoteActionCreators.UpdateNote(content, issuer_id, note_id))); },
	    deleteNote: function (note_id) {
	        dispatch(ModalActionCreators_1.ModalActionCreators.showConfirmModal("Вы действительно хотите удалить заметку?", IssuerNoteActionCreators_1.IssuerNoteActionCreators.DeleteNote(note_id)));
	    },
	    addNewNote: function (note) { return (dispatch(IssuerNoteActionCreators_1.IssuerNoteActionCreators.AddNewNote(note))); },
	    deleteNewNote: function (note_id) { return (dispatch(IssuerNoteActionCreators_1.IssuerNoteActionCreators.DeleteNewNote(note_id))); }
	}); };
	Object.defineProperty(exports, "__esModule", { value: true });
	exports.default = react_redux_1.connect(mapStateToProps, mapDispatchToProps)(IssuerUserNotesView_1.default);


/***/ },
/* 63 */
/***/ function(module, exports, __webpack_require__) {

	"use strict";
	var __extends = (this && this.__extends) || function (d, b) {
	    for (var p in b) if (b.hasOwnProperty(p)) d[p] = b[p];
	    function __() { this.constructor = d; }
	    d.prototype = b === null ? Object.create(b) : (__.prototype = b.prototype, new __());
	};
	var React = __webpack_require__(1);
	var EditableSpan_1 = __webpack_require__(64);
	var Utilites_1 = __webpack_require__(48);
	;
	;
	var IssuerUserNotes = (function (_super) {
	    __extends(IssuerUserNotes, _super);
	    function IssuerUserNotes() {
	        return _super.apply(this, arguments) || this;
	    }
	    IssuerUserNotes.prototype.componentDidMount = function () {
	        this.props.getNotes(this.props.issuer_id);
	    };
	    IssuerUserNotes.prototype.changeNoteContent = function (note_id, content) {
	        if (!content || content.trim() == "") {
	            return this.props.deleteNote(note_id);
	        }
	        this.props.updadeNote(content, this.props.issuer_id, note_id);
	    };
	    IssuerUserNotes.prototype.saveNewNote = function (note_id, content) {
	        if (!content || content.trim() == "") {
	            return this.props.deleteNewNote(note_id);
	        }
	        this.props.saveNote(note_id, content, this.props.issuer_id, this.props.can_use_cloud);
	    };
	    IssuerUserNotes.prototype.addNewNote = function () {
	        var id = Utilites_1.GenerateGUID();
	        this.props.addNewNote({
	            id: id,
	            user_id: null,
	            issuer_id: this.props.issuer_id
	        });
	    };
	    IssuerUserNotes.prototype.render = function () {
	        var _this = this;
	        var notes = this.props.notes.map(function (note) {
	            return (React.createElement("tr", { key: note.id, className: "note_item" },
	                React.createElement("td", null,
	                    React.createElement("span", { className: "ddel", onClick: _this.props.deleteNote.bind(_this, note.id) },
	                        React.createElement("span", { className: "ddel_1" },
	                            React.createElement("b", null, "x"),
	                            "\u0423\u0434\u0430\u043B\u0438\u0442\u044C")),
	                    React.createElement(EditableSpan_1.default, { containerClassName: "notePlaceholder", id: note.id, text: note.content, changeNote: _this.changeNoteContent.bind(_this, note.id), is_in_editmode: false }),
	                    React.createElement("div", { className: "link_block_info" },
	                        React.createElement("span", { className: "explain" },
	                            "\u0414\u0430\u0442\u0430 \u0438\u0437\u043C\u0435\u043D\u0435\u043D\u0438\u044F:",
	                            note.update_date)))));
	        });
	        var new_notes = this.props.new_notes.map(function (note) {
	            var id = String(note.id);
	            return (React.createElement("tr", { key: id, className: "note_item" },
	                React.createElement("td", null,
	                    React.createElement("div", { className: "cloud_warning" }, "\u041D\u0435 \u0441\u043E\u0445\u0440\u0430\u043D\u0435\u043D\u043E!"),
	                    React.createElement(EditableSpan_1.default, { containerClassName: "notePlaceholder", id: id, text: note.content, changeNote: _this.saveNewNote.bind(_this, id), is_in_editmode: true }))));
	        });
	        return (React.createElement("div", { className: "link_block", id: "lb_2" },
	            React.createElement("h3", { className: "cloud_switcher_header" },
	                React.createElement("span", { className: "icon-sticky-note-o" }),
	                "\u0417\u0430\u043C\u0435\u0442\u043A\u0438",
	                React.createElement("span", { className: "icon-angle-down cloud_switcher_ar" })),
	            React.createElement("table", { className: "extra_table", style: { border: 0 }, cellPadding: "0", cellSpacing: "0" },
	                React.createElement("tbody", null,
	                    React.createElement("tr", null,
	                        React.createElement("td", null,
	                            React.createElement("div", { className: "icon_block large", onClick: this.addNewNote.bind(this) },
	                                React.createElement("span", { className: "icon-plus new ico" }),
	                                React.createElement("span", null, "\u0414\u043E\u0431\u0430\u0432\u0438\u0442\u044C")))),
	                    new_notes,
	                    notes))));
	    };
	    return IssuerUserNotes;
	}(React.Component));
	Object.defineProperty(exports, "__esModule", { value: true });
	exports.default = IssuerUserNotes;


/***/ },
/* 64 */
/***/ function(module, exports, __webpack_require__) {

	"use strict";
	var __extends = (this && this.__extends) || function (d, b) {
	    for (var p in b) if (b.hasOwnProperty(p)) d[p] = b[p];
	    function __() { this.constructor = d; }
	    d.prototype = b === null ? Object.create(b) : (__.prototype = b.prototype, new __());
	};
	var React = __webpack_require__(1);
	;
	;
	var EditableSpan = (function (_super) {
	    __extends(EditableSpan, _super);
	    function EditableSpan(props) {
	        var _this = _super.call(this, props) || this;
	        _this.state = {
	            current_text: props.text,
	            is_in_editmode: props.is_in_editmode
	        };
	        return _this;
	    }
	    EditableSpan.prototype.editSpan = function (is_edit) {
	        this.setState({ is_in_editmode: is_edit });
	    };
	    EditableSpan.prototype.changeText = function (event) {
	        this.setState({ current_text: event.target.value });
	    };
	    EditableSpan.prototype.cancelChange = function () {
	        this.setState({ current_text: this.props.text });
	    };
	    EditableSpan.prototype.submitChange = function () {
	        this.props.changeNote(this.state.current_text);
	    };
	    EditableSpan.prototype.render = function () {
	        var clName = this.props.containerClassName === null ? "" : this.props.containerClassName;
	        return (React.createElement("div", { className: clName }, this.state.is_in_editmode ?
	            React.createElement("div", null,
	                React.createElement("div", { className: "form-group" },
	                    React.createElement("textarea", { className: "form-control", autoFocus: true, value: this.state.current_text, onBlur: this.editSpan.bind(this, false), onChange: this.changeText.bind(this) })),
	                React.createElement("div", { className: "form-group" },
	                    React.createElement("button", { className: "btns darkblue", onMouseDown: this.submitChange.bind(this) }, "\u0421\u043E\u0445\u0440\u0430\u043D\u0438\u0442\u044C"),
	                    React.createElement("button", { className: "btns grey", onMouseDown: this.cancelChange.bind(this) }, "\u041E\u0442\u043C\u0435\u043D\u0430")))
	            :
	                React.createElement("div", { className: "spanContent", onClick: this.editSpan.bind(this, true) }, this.props.text)));
	    };
	    return EditableSpan;
	}(React.Component));
	Object.defineProperty(exports, "__esModule", { value: true });
	exports.default = EditableSpan;


/***/ },
/* 65 */
/***/ function(module, exports, __webpack_require__) {

	"use strict";
	var __awaiter = (this && this.__awaiter) || function (thisArg, _arguments, P, generator) {
	    return new (P || (P = Promise))(function (resolve, reject) {
	        function fulfilled(value) { try { step(generator.next(value)); } catch (e) { reject(e); } }
	        function rejected(value) { try { step(generator["throw"](value)); } catch (e) { reject(e); } }
	        function step(result) { result.done ? resolve(result.value) : new P(function (resolve) { resolve(result.value); }).then(fulfilled, rejected); }
	        step((generator = generator.apply(thisArg, _arguments)).next());
	    });
	};
	var __generator = (this && this.__generator) || function (thisArg, body) {
	    var _ = { label: 0, sent: function() { if (t[0] & 1) throw t[1]; return t[1]; }, trys: [], ops: [] }, f, y, t;
	    return { next: verb(0), "throw": verb(1), "return": verb(2) };
	    function verb(n) { return function (v) { return step([n, v]); }; }
	    function step(op) {
	        if (f) throw new TypeError("Generator is already executing.");
	        while (_) try {
	            if (f = 1, y && (t = y[op[0] & 2 ? "return" : op[0] ? "throw" : "next"]) && !(t = t.call(y, op[1])).done) return t;
	            if (y = 0, t) op = [0, t.value];
	            switch (op[0]) {
	                case 0: case 1: t = op; break;
	                case 4: _.label++; return { value: op[1], done: false };
	                case 5: _.label++; y = op[1]; op = [0]; continue;
	                case 7: op = _.ops.pop(); _.trys.pop(); continue;
	                default:
	                    if (!(t = _.trys, t = t.length > 0 && t[t.length - 1]) && (op[0] === 6 || op[0] === 2)) { _ = 0; continue; }
	                    if (op[0] === 3 && (!t || (op[1] > t[0] && op[1] < t[3]))) { _.label = op[1]; break; }
	                    if (op[0] === 6 && _.label < t[1]) { _.label = t[1]; t = op; break; }
	                    if (t && _.label < t[2]) { _.label = t[2]; _.ops.push(op); break; }
	                    if (t[2]) _.ops.pop();
	                    _.trys.pop(); continue;
	            }
	            op = body.call(thisArg, _);
	        } catch (e) { op = [6, e]; y = 0; } finally { f = t = 0; }
	        if (op[0] & 5) throw op[1]; return { value: op[0] ? op[1] : void 0, done: true };
	    }
	};
	var Constants_1 = __webpack_require__(47);
	var IssuerUserNotesAPI_1 = __webpack_require__(66);
	var ModalActionCreators_1 = __webpack_require__(59);
	var IssuerNoteActionCreators = (function () {
	    function IssuerNoteActionCreators() {
	    }
	    IssuerNoteActionCreators._requestNotes = function () {
	        return { type: Constants_1.REQUEST_NOTES };
	    };
	    IssuerNoteActionCreators._recieveNotes = function (notes) {
	        return { type: Constants_1.RECEIVE_NOTES, notes: notes };
	    };
	    IssuerNoteActionCreators.GetNotes = function (issuer_id) {
	        var _this = this;
	        return function (dispatch) { return __awaiter(_this, void 0, void 0, function () {
	            var notes, error_1;
	            return __generator(this, function (_a) {
	                switch (_a.label) {
	                    case 0:
	                        _a.trys.push([0, 2, , 3]);
	                        dispatch(this._requestNotes());
	                        return [4 /*yield*/, IssuerUserNotesAPI_1.default.Get(issuer_id)];
	                    case 1:
	                        notes = _a.sent();
	                        dispatch(this._recieveNotes(notes));
	                        return [3 /*break*/, 3];
	                    case 2:
	                        error_1 = _a.sent();
	                        dispatch(ModalActionCreators_1.ModalActionCreators.showModal("Ошибка загрузки заметок: " + (error_1.message || error_1)));
	                        return [3 /*break*/, 3];
	                    case 3: return [2 /*return*/];
	                }
	            });
	        }); };
	    };
	    IssuerNoteActionCreators._requestUpdateNote = function () {
	        return { type: Constants_1.REQUEST_UPDATE_NOTE };
	    };
	    IssuerNoteActionCreators._recieveUpdateNote = function (note) {
	        return { type: Constants_1.RECEIVE_UPDATE_NOTE, note: note };
	    };
	    IssuerNoteActionCreators.UpdateNote = function (content, issuer_id, note_id) {
	        var _this = this;
	        return function (dispatch) { return __awaiter(_this, void 0, void 0, function () {
	            var note, error_2;
	            return __generator(this, function (_a) {
	                switch (_a.label) {
	                    case 0:
	                        _a.trys.push([0, 2, , 3]);
	                        dispatch(this._requestUpdateNote());
	                        return [4 /*yield*/, IssuerUserNotesAPI_1.default.Update(content, issuer_id, note_id)];
	                    case 1:
	                        note = _a.sent();
	                        dispatch(this._recieveUpdateNote(note));
	                        return [3 /*break*/, 3];
	                    case 2:
	                        error_2 = _a.sent();
	                        dispatch(ModalActionCreators_1.ModalActionCreators.showModal("Ошибка обновления заметки: " + (error_2.message || error_2)));
	                        return [3 /*break*/, 3];
	                    case 3: return [2 /*return*/];
	                }
	            });
	        }); };
	    };
	    IssuerNoteActionCreators._requestAddNote = function () {
	        return { type: Constants_1.REQUEST_ADD_NOTE };
	    };
	    IssuerNoteActionCreators._recieveAddNote = function (note) {
	        return { type: Constants_1.RECEIVE_ADD_NOTE, note: note };
	    };
	    IssuerNoteActionCreators.AddNote = function (note_id, content, issuer_id) {
	        var _this = this;
	        return function (dispatch) { return __awaiter(_this, void 0, void 0, function () {
	            var note, error_3;
	            return __generator(this, function (_a) {
	                switch (_a.label) {
	                    case 0:
	                        _a.trys.push([0, 2, , 3]);
	                        dispatch(this._requestAddNote());
	                        return [4 /*yield*/, IssuerUserNotesAPI_1.default.Update(content, issuer_id)];
	                    case 1:
	                        note = _a.sent();
	                        dispatch(this._recieveAddNote(note));
	                        dispatch(this.DeleteNewNote(note_id));
	                        return [3 /*break*/, 3];
	                    case 2:
	                        error_3 = _a.sent();
	                        dispatch(ModalActionCreators_1.ModalActionCreators.showModal("Ошибка добавления заметки: " + (error_3.message || error_3)));
	                        return [3 /*break*/, 3];
	                    case 3: return [2 /*return*/];
	                }
	            });
	        }); };
	    };
	    IssuerNoteActionCreators._requestDeleteNote = function () {
	        return { type: Constants_1.REQUEST_DELETE_NOTE };
	    };
	    IssuerNoteActionCreators._recieveDeleteNote = function (note) {
	        return { type: Constants_1.RECEIVE_DELETE_NOTE, note: note };
	    };
	    IssuerNoteActionCreators.DeleteNote = function (note_id) {
	        var _this = this;
	        return function (dispatch) { return __awaiter(_this, void 0, void 0, function () {
	            var note, error_4;
	            return __generator(this, function (_a) {
	                switch (_a.label) {
	                    case 0:
	                        _a.trys.push([0, 2, , 3]);
	                        dispatch(this._requestDeleteNote());
	                        return [4 /*yield*/, IssuerUserNotesAPI_1.default.Delete(note_id)];
	                    case 1:
	                        note = _a.sent();
	                        dispatch(this._recieveDeleteNote(note));
	                        return [3 /*break*/, 3];
	                    case 2:
	                        error_4 = _a.sent();
	                        dispatch(ModalActionCreators_1.ModalActionCreators.showModal("Ошибка удаления заметки: " + (error_4.message || error_4)));
	                        return [3 /*break*/, 3];
	                    case 3: return [2 /*return*/];
	                }
	            });
	        }); };
	    };
	    IssuerNoteActionCreators.AddNewNote = function (note) {
	        return { type: Constants_1.ADD_NEW_NOTE, note: note };
	    };
	    IssuerNoteActionCreators.DeleteNewNote = function (note_id) {
	        return { type: Constants_1.DELETE_NEW_NOTE, id: note_id };
	    };
	    return IssuerNoteActionCreators;
	}());
	exports.IssuerNoteActionCreators = IssuerNoteActionCreators;


/***/ },
/* 66 */
/***/ function(module, exports) {

	"use strict";
	var IssuerUserNotesAPI = (function () {
	    function IssuerUserNotesAPI() {
	    }
	    IssuerUserNotesAPI.Get = function (issuer_id) {
	        return new Promise(function (resolve, reject) {
	            var xhr = new XMLHttpRequest();
	            var params = 'issuer_id=' + issuer_id;
	            xhr.open('GET', '/UserNote/Get?' + params, true);
	            xhr.send();
	            xhr.onreadystatechange = function () {
	                if (this.readyState != 4) {
	                    return;
	                }
	                if (this.status != 200) {
	                    reject(new Error('ошибка: ' + (this.status ? this.statusText : 'запрос не удался')));
	                }
	                resolve(JSON.parse(this.responseText));
	            };
	        });
	    };
	    IssuerUserNotesAPI.Update = function (content, issuer_id, note_id) {
	        if (note_id === void 0) { note_id = null; }
	        return new Promise(function (resolve, reject) {
	            var u_note = {
	                id: note_id,
	                content: content,
	                user_id: null,
	                issuer_id: issuer_id,
	                update_date: null
	            };
	            var xhr = new XMLHttpRequest();
	            xhr.open('POST', '/UserNote/Update', true);
	            xhr.setRequestHeader('Content-type', 'application/json; charset=utf-8');
	            xhr.send(JSON.stringify(u_note));
	            xhr.onreadystatechange = function () {
	                if (this.readyState != 4) {
	                    return;
	                }
	                if (this.status != 200) {
	                    reject(new Error('ошибка: ' + (this.status ? this.statusText : 'запрос не удался')));
	                }
	                resolve(JSON.parse(this.responseText));
	            };
	        });
	    };
	    IssuerUserNotesAPI.Delete = function (note_id) {
	        return new Promise(function (resolve, reject) {
	            var xhr = new XMLHttpRequest();
	            var params = 'note_id=' + note_id;
	            xhr.open('GET', '/UserNote/Delete?' + params, true);
	            xhr.send();
	            xhr.onreadystatechange = function () {
	                if (this.readyState != 4) {
	                    return;
	                }
	                if (this.status != 200) {
	                    reject(new Error('ошибка: ' + (this.status ? this.statusText : 'запрос не удался')));
	                }
	                resolve(JSON.parse(this.responseText));
	            };
	        });
	    };
	    return IssuerUserNotesAPI;
	}());
	Object.defineProperty(exports, "__esModule", { value: true });
	exports.default = IssuerUserNotesAPI;


/***/ },
/* 67 */
/***/ function(module, exports, __webpack_require__) {

	"use strict";
	var react_redux_1 = __webpack_require__(3);
	var IssuerUserAddressView_1 = __webpack_require__(68);
	var IssuerAddressActionCreators_1 = __webpack_require__(71);
	var ModalActionCreators_1 = __webpack_require__(59);
	var AccessCloudActionCreators_1 = __webpack_require__(60);
	var mapStateToProps = function (state) { return ({
	    addresses: state.addresses,
	    issuer_id: state.issuer_id,
	    can_use_cloud: state.can_use_cloud
	}); };
	var mapDispatchToProps = function (dispatch) { return ({
	    getAddresses: function (issuer_id) { return (dispatch(IssuerAddressActionCreators_1.IssuerAddressActionCreators.GetAddresses(issuer_id))); },
	    saveAddress: function (address, can_use_cloud) {
	        if (can_use_cloud) {
	            dispatch(IssuerAddressActionCreators_1.IssuerAddressActionCreators.UpdateAddress(address));
	        }
	        else {
	            dispatch(AccessCloudActionCreators_1.AccessCloudActionCreators.showModal(IssuerAddressActionCreators_1.IssuerAddressActionCreators.UpdateAddress(address)));
	        }
	    },
	    deleteAddress: function (address_id, address_name) {
	        dispatch(ModalActionCreators_1.ModalActionCreators.showConfirmModal("Вы действительно хотите удалить контакт: " + address_name + "?", IssuerAddressActionCreators_1.IssuerAddressActionCreators.DeleteAddress(address_id)));
	    }
	}); };
	Object.defineProperty(exports, "__esModule", { value: true });
	exports.default = react_redux_1.connect(mapStateToProps, mapDispatchToProps)(IssuerUserAddressView_1.default);


/***/ },
/* 68 */
/***/ function(module, exports, __webpack_require__) {

	"use strict";
	var __extends = (this && this.__extends) || function (d, b) {
	    for (var p in b) if (b.hasOwnProperty(p)) d[p] = b[p];
	    function __() { this.constructor = d; }
	    d.prototype = b === null ? Object.create(b) : (__.prototype = b.prototype, new __());
	};
	var React = __webpack_require__(1);
	var AddressForm_1 = __webpack_require__(69);
	;
	;
	var IssuerUserAddress = (function (_super) {
	    __extends(IssuerUserAddress, _super);
	    function IssuerUserAddress() {
	        var _this = _super.apply(this, arguments) || this;
	        _this.is_load_data = false;
	        return _this;
	    }
	    IssuerUserAddress.prototype.componentDidMount = function () {
	        this.props.getAddresses(this.props.issuer_id);
	        this.is_load_data = true;
	    };
	    IssuerUserAddress.prototype.updateAddress = function (address) {
	        this.props.saveAddress(address, this.props.can_use_cloud);
	    };
	    IssuerUserAddress.prototype.render = function () {
	        var _this = this;
	        var addresses = this.props.addresses.map(function (address) {
	            return (React.createElement(AddressForm_1.default, { address: address, show_details: false, key: address.id, issuer_id: _this.props.issuer_id, deleteAddress: _this.props.deleteAddress.bind(_this), updateAddress: _this.updateAddress.bind(_this) }));
	        });
	        return (React.createElement("div", { className: "link_block", id: "lb_0" },
	            React.createElement("h3", { className: "cloud_switcher_header" },
	                React.createElement("span", { className: "icon-users" }),
	                "\u0421\u043F\u0438\u0441\u043E\u043A \u043A\u043E\u043D\u0442\u0430\u043A\u0442\u043E\u0432",
	                React.createElement("span", { className: "icon-angle-down cloud_switcher_ar" })),
	            React.createElement("table", { className: "extra_table", style: { border: 0 }, cellPadding: "0", cellSpacing: "0" },
	                React.createElement("tbody", null,
	                    addresses,
	                    React.createElement(AddressForm_1.default, { key: 0, address: null, show_details: this.is_load_data && this.props.addresses.length == 0, updateAddress: this.updateAddress.bind(this), issuer_id: this.props.issuer_id, deleteAddress: this.props.deleteAddress.bind(null) })))));
	    };
	    return IssuerUserAddress;
	}(React.Component));
	Object.defineProperty(exports, "__esModule", { value: true });
	exports.default = IssuerUserAddress;


/***/ },
/* 69 */
/***/ function(module, exports, __webpack_require__) {

	"use strict";
	var __extends = (this && this.__extends) || function (d, b) {
	    for (var p in b) if (b.hasOwnProperty(p)) d[p] = b[p];
	    function __() { this.constructor = d; }
	    d.prototype = b === null ? Object.create(b) : (__.prototype = b.prototype, new __());
	};
	var __assign = (this && this.__assign) || Object.assign || function(t) {
	    for (var s, i = 1, n = arguments.length; i < n; i++) {
	        s = arguments[i];
	        for (var p in s) if (Object.prototype.hasOwnProperty.call(s, p))
	            t[p] = s[p];
	    }
	    return t;
	};
	var React = __webpack_require__(1);
	var IAddress_1 = __webpack_require__(70);
	;
	;
	var AddressForm = (function (_super) {
	    __extends(AddressForm, _super);
	    function AddressForm(props) {
	        var _this = _super.call(this, props) || this;
	        _this.is_new = _this.props.address == null;
	        var address = !_this.is_new ? __assign({}, _this.props.address) : _this.generateNewAddress();
	        _this.state = {
	            edit_address: address,
	            show_details: _this.props.show_details
	        };
	        return _this;
	        //console.log('constructor. State id:'+this.state.edit_address.id+", props_id:"+ this.props.address.id);
	    }
	    AddressForm.prototype.componentWillReceiveProps = function (nextProps) {
	        this.setState({
	            show_details: nextProps.show_details
	        });
	    };
	    AddressForm.prototype.generateNewAddress = function () {
	        return {
	            id: null,
	            user_id: null,
	            issuer_id: this.props.issuer_id,
	            name: '',
	            phone: '',
	            email: '',
	            note: '',
	            update_date: null,
	            extrafields: []
	        };
	    };
	    AddressForm.prototype.generateSelector = function (selected_val, id) {
	        var options = [];
	        for (var i = 0; i < IAddress_1.AddressFieldKeys.length; i++) {
	            options.push(React.createElement("option", { value: IAddress_1.AddressFieldKeys[i].id, key: i }, IAddress_1.AddressFieldKeys[i].name));
	        }
	        return (React.createElement("select", { className: "form-control", value: selected_val, onChange: this.changeExtraFieldType.bind(this, id) }, options));
	    };
	    AddressForm.prototype.changeFieldValue = function (field_name, event) {
	        var addr = this.state.edit_address;
	        addr[field_name] = event.target.value;
	        this.setState({
	            edit_address: addr
	        });
	    };
	    AddressForm.prototype.changeExtraFieldValue = function (i, event) {
	        var addr = this.state.edit_address;
	        addr.extrafields[i].value = event.target.value;
	        this.setState({
	            edit_address: addr
	        });
	    };
	    AddressForm.prototype.changeExtraFieldType = function (i, event) {
	        var addr = this.state.edit_address;
	        addr.extrafields[i].key = event.target.value;
	        this.setState({
	            edit_address: addr
	        });
	    };
	    AddressForm.prototype.clearForm = function (event) {
	        this.setState({
	            edit_address: this.generateNewAddress(),
	            show_details: false
	        });
	        event.preventDefault();
	    };
	    AddressForm.prototype.resetForm = function (event) {
	        this.setState({
	            edit_address: __assign({}, this.props.address),
	            show_details: false
	        });
	        event.preventDefault();
	    };
	    AddressForm.prototype.submitChange = function (event) {
	        if (this.state.edit_address.name !== "") {
	            //удалим дополнительные пустые поля
	            var addr = this.state.edit_address;
	            for (var i = 0; i < addr.extrafields.length; i++) {
	                if (addr.extrafields[i].value == "") {
	                    addr.extrafields.splice(i, 1);
	                }
	            }
	            this.setState({
	                edit_address: addr
	            });
	            this.props.updateAddress(this.state.edit_address);
	            if (this.is_new) {
	                this.setState({
	                    edit_address: this.generateNewAddress()
	                });
	            }
	            else {
	                this.setState({
	                    show_details: false
	                });
	            }
	        }
	        event.preventDefault();
	    };
	    AddressForm.prototype.showDetails = function (event) {
	        this.setState({
	            show_details: true
	        });
	        event.preventDefault();
	    };
	    AddressForm.prototype.addExtraField = function (event) {
	        var addr = this.state.edit_address;
	        addr.extrafields.push({ key: 1, value: "" });
	        this.setState({
	            edit_address: addr
	        });
	        event.preventDefault();
	    };
	    AddressForm.prototype.removeExtraField = function (id, event) {
	        var addr = this.state.edit_address;
	        addr.extrafields.splice(id, 1);
	        this.setState({
	            edit_address: addr
	        });
	        event.preventDefault();
	    };
	    AddressForm.prototype.showHintInfo = function () {
	        if (this.is_new) {
	            return null;
	        }
	        var infos = [];
	        if (this.props.address.name != "") {
	            infos.push("Имя: " + this.props.address.name);
	        }
	        if (this.props.address.phone != "" && this.props.address.phone != null) {
	            infos.push("Телефон: " + this.props.address.phone);
	        }
	        if (this.props.address.email != "" && this.props.address.email != null) {
	            infos.push("Email: " + this.props.address.email);
	        }
	        var _loop_1 = function (i) {
	            var e_info = this_1.props.address.extrafields[i];
	            infos.push(IAddress_1.AddressFieldKeys.filter(function (p) { return p.id == e_info.key; })[0].name + ": " + e_info.value);
	        };
	        var this_1 = this;
	        for (var i = 0; i < this.props.address.extrafields.length; i++) {
	            _loop_1(i);
	        }
	        if (this.props.address.note != "") {
	            infos.push(this.props.address.note);
	        }
	        return React.createElement("em", null, infos.map(function (val, i, arr) {
	            var divider = i < arr.length - 1 ? React.createElement("br", null) : null;
	            return (React.createElement("span", null,
	                val,
	                divider));
	        }));
	    };
	    AddressForm.prototype.render = function () {
	        //  console.log('render. State id:'+this.state.edit_address.id+", props_id:"+ this.props.address.id);
	        var form_item = [];
	        if (this.state.edit_address.extrafields != null) {
	            var fields = this.state.edit_address.extrafields;
	            for (var i = 0; i < fields.length; i++) {
	                form_item.push(React.createElement("div", { className: "form-group", key: i },
	                    React.createElement("div", { className: "extrafields_block_item" },
	                        React.createElement("div", { className: "extrafields_block_item_left" },
	                            this.generateSelector(fields[i].key, i),
	                            React.createElement("input", { id: "val" + i, type: "text", className: "form-control", placeholder: "Значение", onChange: this.changeExtraFieldValue.bind(this, i), value: fields[i].value })),
	                        React.createElement("div", { className: "extrafields_block_item_right" },
	                            React.createElement("button", { className: "btns red", onClick: this.removeExtraField.bind(this, i) }, "-")))));
	            }
	        }
	        var form = this.state.show_details ?
	            (React.createElement("form", null,
	                React.createElement("div", { className: "form-group" },
	                    React.createElement("label", { htmlFor: "name", className: "label_form" }, "\u0418\u043C\u044F"),
	                    React.createElement("div", { className: "sub_form" },
	                        React.createElement("input", { id: "name", type: "text", className: "form-control", placeholder: "Имя", onChange: this.changeFieldValue.bind(this, 'name'), value: this.state.edit_address.name }))),
	                React.createElement("div", { className: "form-group" },
	                    React.createElement("label", { htmlFor: "phone", className: "label_form" }, "\u0422\u0435\u043B\u0435\u0444\u043E\u043D"),
	                    React.createElement("div", { className: "sub_form" },
	                        React.createElement("input", { id: "phone", type: "text", className: "form-control", placeholder: "Телефон", onChange: this.changeFieldValue.bind(this, 'phone'), value: this.state.edit_address.phone }))),
	                React.createElement("div", { className: "form-group" },
	                    React.createElement("label", { htmlFor: "email", className: "label_form" }, "Email"),
	                    React.createElement("div", { className: "sub_form" },
	                        React.createElement("input", { id: "email", type: "text", className: "form-control", placeholder: "Email", onChange: this.changeFieldValue.bind(this, 'email'), value: this.state.edit_address.email }))),
	                React.createElement("div", { className: "form-group" },
	                    React.createElement("textarea", { id: "note", className: "form-control", placeholder: "Примечание", onChange: this.changeFieldValue.bind(this, 'note'), value: this.state.edit_address.note })),
	                React.createElement("h5", null, "\u0414\u043E\u043F\u043E\u043B\u043D\u0438\u0442\u0435\u043B\u044C\u043D\u0430\u044F \u0438\u043D\u0444\u043E\u0440\u043C\u0430\u0446\u0438\u044F"),
	                form_item,
	                React.createElement("div", { className: "form-group" },
	                    React.createElement("button", { className: "btns red", onClick: this.addExtraField.bind(this) }, "+")),
	                React.createElement("div", { className: "form-group" },
	                    React.createElement("button", { className: this.state.edit_address.name === "" ? "btns darkblue disabled" : "btns darkblue", onClick: this.submitChange.bind(this) }, "\u0421\u043E\u0445\u0440\u0430\u043D\u0438\u0442\u044C"),
	                    this.is_new ?
	                        React.createElement("button", { className: "btns grey", onClick: this.clearForm.bind(this) }, "\u041E\u0442\u043C\u0435\u043D\u0430")
	                        :
	                            React.createElement("button", { className: "btns grey", onClick: this.resetForm.bind(this) }, "\u041E\u0442\u043C\u0435\u043D\u0430")))) : null;
	        var header = this.state.show_details ?
	            (React.createElement("h4", null, this.is_new ? "Новый контакт" : this.props.address.name))
	            :
	                (React.createElement("a", { href: "#", onClick: this.showDetails.bind(this) },
	                    React.createElement("span", { className: "title" },
	                        this.is_new ? "Добавить контакт" : this.props.address.name,
	                        this.showHintInfo())));
	        var delete_block = this.is_new ? null : (React.createElement("span", { className: "ddel", onClick: this.props.deleteAddress.bind(null, this.props.address.id, this.props.address.name) },
	            React.createElement("span", { className: "ddel_1" },
	                React.createElement("b", null, "x"),
	                "\u0423\u0434\u0430\u043B\u0438\u0442\u044C")));
	        return (React.createElement("tr", { key: this.state.edit_address.id, className: "address_item" },
	            React.createElement("td", null,
	                delete_block,
	                React.createElement("div", { className: "icon_block large address_title" },
	                    this.is_new && !this.state.show_details ?
	                        React.createElement("span", { className: "icon-plus new ico" })
	                        :
	                            React.createElement("span", { className: "icon-address-card-o link ico" }),
	                    header),
	                form)));
	    };
	    return AddressForm;
	}(React.Component));
	Object.defineProperty(exports, "__esModule", { value: true });
	exports.default = AddressForm;


/***/ },
/* 70 */
/***/ function(module, exports) {

	"use strict";
	exports.AddressFieldKeys = [
	    {
	        id: 1,
	        name: "Должность"
	    },
	    {
	        id: 2,
	        name: "Веб-сайт"
	    },
	    {
	        id: 3,
	        name: "Адрес"
	    },
	    {
	        id: 4,
	        name: "Домашний телефон"
	    },
	    {
	        id: 5,
	        name: "Мобильный телефон"
	    }
	];


/***/ },
/* 71 */
/***/ function(module, exports, __webpack_require__) {

	"use strict";
	var __awaiter = (this && this.__awaiter) || function (thisArg, _arguments, P, generator) {
	    return new (P || (P = Promise))(function (resolve, reject) {
	        function fulfilled(value) { try { step(generator.next(value)); } catch (e) { reject(e); } }
	        function rejected(value) { try { step(generator["throw"](value)); } catch (e) { reject(e); } }
	        function step(result) { result.done ? resolve(result.value) : new P(function (resolve) { resolve(result.value); }).then(fulfilled, rejected); }
	        step((generator = generator.apply(thisArg, _arguments)).next());
	    });
	};
	var __generator = (this && this.__generator) || function (thisArg, body) {
	    var _ = { label: 0, sent: function() { if (t[0] & 1) throw t[1]; return t[1]; }, trys: [], ops: [] }, f, y, t;
	    return { next: verb(0), "throw": verb(1), "return": verb(2) };
	    function verb(n) { return function (v) { return step([n, v]); }; }
	    function step(op) {
	        if (f) throw new TypeError("Generator is already executing.");
	        while (_) try {
	            if (f = 1, y && (t = y[op[0] & 2 ? "return" : op[0] ? "throw" : "next"]) && !(t = t.call(y, op[1])).done) return t;
	            if (y = 0, t) op = [0, t.value];
	            switch (op[0]) {
	                case 0: case 1: t = op; break;
	                case 4: _.label++; return { value: op[1], done: false };
	                case 5: _.label++; y = op[1]; op = [0]; continue;
	                case 7: op = _.ops.pop(); _.trys.pop(); continue;
	                default:
	                    if (!(t = _.trys, t = t.length > 0 && t[t.length - 1]) && (op[0] === 6 || op[0] === 2)) { _ = 0; continue; }
	                    if (op[0] === 3 && (!t || (op[1] > t[0] && op[1] < t[3]))) { _.label = op[1]; break; }
	                    if (op[0] === 6 && _.label < t[1]) { _.label = t[1]; t = op; break; }
	                    if (t && _.label < t[2]) { _.label = t[2]; _.ops.push(op); break; }
	                    if (t[2]) _.ops.pop();
	                    _.trys.pop(); continue;
	            }
	            op = body.call(thisArg, _);
	        } catch (e) { op = [6, e]; y = 0; } finally { f = t = 0; }
	        if (op[0] & 5) throw op[1]; return { value: op[0] ? op[1] : void 0, done: true };
	    }
	};
	var Constants_1 = __webpack_require__(47);
	var IssuerUserAddressesAPI_1 = __webpack_require__(72);
	var ModalActionCreators_1 = __webpack_require__(59);
	var IssuerAddressActionCreators = (function () {
	    function IssuerAddressActionCreators() {
	    }
	    IssuerAddressActionCreators._requestAddresses = function () {
	        return { type: Constants_1.REQUEST_ADDRESSES };
	    };
	    IssuerAddressActionCreators._recieveAddresses = function (addresses) {
	        return { type: Constants_1.RECEIVE_ADDRESSES, addresses: addresses };
	    };
	    IssuerAddressActionCreators.GetAddresses = function (issuer_id) {
	        var _this = this;
	        return function (dispatch) { return __awaiter(_this, void 0, void 0, function () {
	            var addresses, error_1;
	            return __generator(this, function (_a) {
	                switch (_a.label) {
	                    case 0:
	                        _a.trys.push([0, 2, , 3]);
	                        dispatch(this._requestAddresses());
	                        return [4 /*yield*/, IssuerUserAddressesAPI_1.default.Get(issuer_id)];
	                    case 1:
	                        addresses = _a.sent();
	                        dispatch(this._recieveAddresses(addresses));
	                        return [3 /*break*/, 3];
	                    case 2:
	                        error_1 = _a.sent();
	                        dispatch(ModalActionCreators_1.ModalActionCreators.showModal("Ошибка загрузки списка контактов: " + (error_1.message || error_1)));
	                        return [3 /*break*/, 3];
	                    case 3: return [2 /*return*/];
	                }
	            });
	        }); };
	    };
	    IssuerAddressActionCreators._requestUpdateAddress = function () {
	        return { type: Constants_1.REQUEST_UPDATE_ADDRESS };
	    };
	    IssuerAddressActionCreators._recieveUpdateAddress = function (address) {
	        return { type: Constants_1.RECEIVE_UPDATE_ADDRESS, address: address };
	    };
	    IssuerAddressActionCreators.UpdateAddress = function (address) {
	        var _this = this;
	        return function (dispatch) { return __awaiter(_this, void 0, void 0, function () {
	            var addr, error_2;
	            return __generator(this, function (_a) {
	                switch (_a.label) {
	                    case 0:
	                        _a.trys.push([0, 2, , 3]);
	                        dispatch(this._requestUpdateAddress());
	                        return [4 /*yield*/, IssuerUserAddressesAPI_1.default.Update(address)];
	                    case 1:
	                        addr = _a.sent();
	                        dispatch(this._recieveUpdateAddress(addr));
	                        return [3 /*break*/, 3];
	                    case 2:
	                        error_2 = _a.sent();
	                        dispatch(ModalActionCreators_1.ModalActionCreators.showModal("Ошибка обновления контакта: " + (error_2.message || error_2)));
	                        return [3 /*break*/, 3];
	                    case 3: return [2 /*return*/];
	                }
	            });
	        }); };
	    };
	    IssuerAddressActionCreators._requestDeleteAddress = function () {
	        return { type: Constants_1.REQUEST_DELETE_ADDRESS };
	    };
	    IssuerAddressActionCreators._recieveDeleteAddress = function (address) {
	        return { type: Constants_1.RECEIVE_DELETE_ADDRESS, address: address };
	    };
	    IssuerAddressActionCreators.DeleteAddress = function (id) {
	        var _this = this;
	        return function (dispatch) { return __awaiter(_this, void 0, void 0, function () {
	            var address, error_3;
	            return __generator(this, function (_a) {
	                switch (_a.label) {
	                    case 0:
	                        _a.trys.push([0, 2, , 3]);
	                        dispatch(this._requestDeleteAddress());
	                        return [4 /*yield*/, IssuerUserAddressesAPI_1.default.Delete(id)];
	                    case 1:
	                        address = _a.sent();
	                        dispatch(this._recieveDeleteAddress(address));
	                        return [3 /*break*/, 3];
	                    case 2:
	                        error_3 = _a.sent();
	                        dispatch(ModalActionCreators_1.ModalActionCreators.showModal("Ошибка удаления контакта: " + (error_3.message || error_3)));
	                        return [3 /*break*/, 3];
	                    case 3: return [2 /*return*/];
	                }
	            });
	        }); };
	    };
	    return IssuerAddressActionCreators;
	}());
	exports.IssuerAddressActionCreators = IssuerAddressActionCreators;


/***/ },
/* 72 */
/***/ function(module, exports) {

	"use strict";
	var IssuerUserAddressesAPI = (function () {
	    function IssuerUserAddressesAPI() {
	    }
	    IssuerUserAddressesAPI.Get = function (issuer_id) {
	        return new Promise(function (resolve, reject) {
	            var xhr = new XMLHttpRequest();
	            var params = 'issuer_id=' + issuer_id;
	            xhr.open('GET', '/UserAddress/Get?' + params, true);
	            xhr.send();
	            xhr.onreadystatechange = function () {
	                if (this.readyState != 4) {
	                    return;
	                }
	                if (this.status != 200) {
	                    reject(new Error('ошибка: ' + (this.status ? this.statusText : 'запрос не удался')));
	                }
	                resolve(JSON.parse(this.responseText));
	            };
	        });
	    };
	    IssuerUserAddressesAPI.Update = function (address) {
	        return new Promise(function (resolve, reject) {
	            var xhr = new XMLHttpRequest();
	            xhr.open('POST', '/UserAddress/Update', true);
	            xhr.setRequestHeader('Content-type', 'application/json; charset=utf-8');
	            xhr.send(JSON.stringify(address));
	            xhr.onreadystatechange = function () {
	                if (this.readyState != 4) {
	                    return;
	                }
	                if (this.status != 200) {
	                    reject(new Error('ошибка: ' + (this.status ? this.statusText : 'запрос не удался')));
	                }
	                resolve(JSON.parse(this.responseText));
	            };
	        });
	    };
	    IssuerUserAddressesAPI.Delete = function (id) {
	        return new Promise(function (resolve, reject) {
	            var xhr = new XMLHttpRequest();
	            var params = 'address_id=' + id;
	            xhr.open('GET', '/UserAddress/Delete?' + params, true);
	            xhr.send();
	            xhr.onreadystatechange = function () {
	                if (this.readyState != 4) {
	                    return;
	                }
	                if (this.status != 200) {
	                    reject(new Error('ошибка: ' + (this.status ? this.statusText : 'запрос не удался')));
	                }
	                resolve(JSON.parse(this.responseText));
	            };
	        });
	    };
	    return IssuerUserAddressesAPI;
	}());
	Object.defineProperty(exports, "__esModule", { value: true });
	exports.default = IssuerUserAddressesAPI;


/***/ },
/* 73 */
/***/ function(module, exports, __webpack_require__) {

	"use strict";
	var react_redux_1 = __webpack_require__(3);
	var ModalActionCreators_1 = __webpack_require__(59);
	var ConfirmModalView_1 = __webpack_require__(74);
	var mapStateToProps = function (state) { return ({
	    isShowing: state.modals.isShowing,
	    body: state.modals.body,
	    action: state.modals.action
	}); };
	var mapDispatchToProps = function (dispatch) { return ({
	    hideModal: function () { return dispatch(ModalActionCreators_1.ModalActionCreators.hideModal()); },
	    accept: function (action) {
	        dispatch(ModalActionCreators_1.ModalActionCreators.hideModal());
	        dispatch(action);
	    }
	}); };
	Object.defineProperty(exports, "__esModule", { value: true });
	exports.default = react_redux_1.connect(mapStateToProps, mapDispatchToProps)(ConfirmModalView_1.default);


/***/ },
/* 74 */
/***/ function(module, exports, __webpack_require__) {

	"use strict";
	var __extends = (this && this.__extends) || function (d, b) {
	    for (var p in b) if (b.hasOwnProperty(p)) d[p] = b[p];
	    function __() { this.constructor = d; }
	    d.prototype = b === null ? Object.create(b) : (__.prototype = b.prototype, new __());
	};
	var React = __webpack_require__(1);
	;
	;
	var ConfirmModalView = (function (_super) {
	    __extends(ConfirmModalView, _super);
	    function ConfirmModalView() {
	        return _super.apply(this, arguments) || this;
	    }
	    ConfirmModalView.prototype.render = function () {
	        if (!this.props.isShowing) {
	            return null;
	        }
	        var command_div = (React.createElement("div", { className: "confirm-dialog" },
	            React.createElement("button", { className: "btns darkblue", onClick: this.props.hideModal.bind(this) }, "OK")));
	        if (this.props.action !== null) {
	            command_div = (React.createElement("div", { className: "confirm-dialog" },
	                React.createElement("button", { className: "btns darkblue", onClick: this.props.accept.bind(this, this.props.action) }, "\u041F\u0440\u0438\u043C\u0435\u043D\u0438\u0442\u044C"),
	                React.createElement("button", { className: "btns grey", onClick: this.props.hideModal.bind(this) }, "\u041E\u0442\u043C\u0435\u043D\u0430")));
	        }
	        return (React.createElement("div", null,
	            React.createElement("div", { className: "modal fade in", style: { display: "block" } },
	                React.createElement("div", { className: "modal-dialog" },
	                    React.createElement("div", { className: "modal-content" },
	                        React.createElement("div", { className: "modal-header" },
	                            React.createElement("button", { className: "close", onClick: this.props.hideModal.bind(this) }, "x"),
	                            " "),
	                        React.createElement("div", { className: "modal-body" },
	                            React.createElement("h3", null, this.props.body),
	                            command_div),
	                        React.createElement("div", { className: "modal-footer" })))),
	            React.createElement("div", { className: "modal-backdrop fade in" })));
	    };
	    return ConfirmModalView;
	}(React.Component));
	Object.defineProperty(exports, "__esModule", { value: true });
	exports.default = ConfirmModalView;


/***/ },
/* 75 */
/***/ function(module, exports, __webpack_require__) {

	"use strict";
	var react_redux_1 = __webpack_require__(3);
	var AccessCloudActionCreators_1 = __webpack_require__(60);
	var CanUseCloudCheckView_1 = __webpack_require__(76);
	var mapStateToProps = function (state) { return ({
	    isShowing: state.can_use_cloud_modals.isShowing,
	    action: state.can_use_cloud_modals.action
	}); };
	var mapDispatchToProps = function (dispatch) { return ({
	    checkCloudUsing: function () { return dispatch(AccessCloudActionCreators_1.AccessCloudActionCreators.CanUseCloud()); },
	    hideModal: function () { return dispatch(AccessCloudActionCreators_1.AccessCloudActionCreators.hideModal()); },
	    accept: function (action) { return dispatch(AccessCloudActionCreators_1.AccessCloudActionCreators.ConfirmCanUseCloud(action)); }
	}); };
	Object.defineProperty(exports, "__esModule", { value: true });
	exports.default = react_redux_1.connect(mapStateToProps, mapDispatchToProps)(CanUseCloudCheckView_1.default);


/***/ },
/* 76 */
/***/ function(module, exports, __webpack_require__) {

	"use strict";
	var __extends = (this && this.__extends) || function (d, b) {
	    for (var p in b) if (b.hasOwnProperty(p)) d[p] = b[p];
	    function __() { this.constructor = d; }
	    d.prototype = b === null ? Object.create(b) : (__.prototype = b.prototype, new __());
	};
	var React = __webpack_require__(1);
	var CloudTerms_1 = __webpack_require__(77);
	;
	;
	var CanUseCloudCheckView = (function (_super) {
	    __extends(CanUseCloudCheckView, _super);
	    function CanUseCloudCheckView() {
	        return _super.apply(this, arguments) || this;
	    }
	    CanUseCloudCheckView.prototype.componentDidMount = function () {
	        this.props.checkCloudUsing();
	    };
	    CanUseCloudCheckView.prototype.render = function () {
	        if (!this.props.isShowing) {
	            return null;
	        }
	        return (React.createElement("div", null,
	            React.createElement("div", { className: "modal fade in", style: { display: "block" } },
	                React.createElement("div", { className: "modal-dialog" },
	                    React.createElement("div", { className: "modal-content" },
	                        React.createElement("div", { className: "modal-header" },
	                            React.createElement("button", { className: "close", onClick: this.props.hideModal.bind(this) }, "x"),
	                            " "),
	                        React.createElement("div", { className: "modal-body" },
	                            React.createElement("h3", null, "\u0414\u043B\u044F \u043F\u0440\u043E\u0434\u043E\u043B\u0436\u0435\u043D\u0438\u044F \u0440\u0430\u0431\u043E\u0442\u044B \u0441 \u0441\u0435\u0440\u0432\u0438\u0441\u043E\u043C \"\u0421\u041A\u0420\u0418\u041D \u041E\u0431\u043B\u0430\u043A\u043E\" \u043F\u0440\u043E\u0441\u0438\u043C \u043F\u0440\u0438\u043D\u044F\u0442\u044C \u0443\u0441\u043B\u043E\u0432\u0438\u044F \u043F\u043E\u043B\u044C\u0437\u043E\u0432\u0430\u0442\u0435\u043B\u044C\u0441\u043A\u043E\u0433\u043E \u0441\u043E\u0433\u043B\u0430\u0448\u0435\u043D\u0438\u044F."),
	                            CloudTerms_1.getTerms(),
	                            React.createElement("div", { className: "confirm-dialog" },
	                                React.createElement("button", { className: "btns darkblue", onClick: this.props.accept.bind(this, this.props.action) }, "\u041F\u0440\u0438\u043D\u044F\u0442\u044C"),
	                                React.createElement("button", { className: "btns grey", onClick: this.props.hideModal.bind(this) }, "\u041E\u0442\u043C\u0435\u043D\u0430"))),
	                        React.createElement("div", { className: "modal-footer" })))),
	            React.createElement("div", { className: "modal-backdrop fade in" })));
	    };
	    return CanUseCloudCheckView;
	}(React.Component));
	Object.defineProperty(exports, "__esModule", { value: true });
	exports.default = CanUseCloudCheckView;


/***/ },
/* 77 */
/***/ function(module, exports, __webpack_require__) {

	"use strict";
	var React = __webpack_require__(1);
	function getTerms() {
	    return (React.createElement("div", { id: "cloudterms_panel" },
	        React.createElement("h4", null, "\u041F\u041E\u041B\u042C\u0417\u041E\u0412\u0410\u0422\u0415\u041B\u042C\u0421\u041A\u041E\u0415 \u0421\u041E\u0413\u041B\u0410\u0428\u0415\u041D\u0418\u0415"),
	        React.createElement("p", null, "\u041D\u0430\u0441\u0442\u043E\u044F\u0449\u0435\u0435 \u041F\u043E\u043B\u044C\u0437\u043E\u0432\u0430\u0442\u0435\u043B\u044C\u0441\u043A\u043E\u0435 \u0441\u043E\u0433\u043B\u0430\u0448\u0435\u043D\u0438\u0435 \u0440\u0435\u0433\u0443\u043B\u0438\u0440\u0443\u0435\u0442 \u043E\u0442\u043D\u043E\u0448\u0435\u043D\u0438\u044F \u043C\u0435\u0436\u0434\u0443 \u041F\u0440\u0430\u0432\u043E\u043E\u0431\u043B\u0430\u0434\u0430\u0442\u0435\u043B\u0435\u043C \u043F\u0440\u0430\u0432 \u043D\u0430 \u0431\u0430\u0437\u044B \u0434\u0430\u043D\u043D\u044B\u0445 \u00AB\u0421\u041A\u0420\u0418\u041D\u00BB \u0438 /\u0438\u043B\u0438 \u00AB\u0421\u041A\u0420\u0418\u041D \u041A\u043E\u043D\u0442\u0440\u0430\u0433\u0435\u043D\u0442\u00BB \u0441 \u043E\u0434\u043D\u043E\u0439 \u0441\u0442\u043E\u0440\u043E\u043D\u044B \u0438 \u041F\u043E\u043B\u044C\u0437\u043E\u0432\u0430\u0442\u0435\u043B\u0435\u043C, \u043A\u043E\u0442\u043E\u0440\u043E\u043C\u0443 \u043F\u0440\u0435\u0434\u043E\u0441\u0442\u0430\u0432\u043B\u044F\u044E\u0442\u0441\u044F \u043F\u0440\u0430\u0432\u0430 \u043D\u0430 \u0438\u0441\u043F\u043E\u043B\u044C\u0437\u043E\u0432\u0430\u043D\u0438\u0435 \u0421\u0435\u0440\u0432\u0438\u0441\u0430 \u041F\u0440\u0430\u0432\u043E\u043E\u0431\u043B\u0430\u0434\u0430\u0442\u0435\u043B\u044F \u00AB\u0421\u041A\u0420\u0418\u041D \u041E\u0431\u043B\u0430\u043A\u043E\u00BB \u0432 \u0440\u0430\u043C\u043A\u0430\u0445 \u0437\u0430\u043A\u043B\u044E\u0447\u0435\u043D\u043D\u043E\u0433\u043E \u043C\u0435\u0436\u0434\u0443 \u043D\u0438\u043C\u0438 \u043B\u0438\u0446\u0435\u043D\u0437\u0438\u043E\u043D\u043D\u043E\u0433\u043E \u0434\u043E\u0433\u043E\u0432\u043E\u0440\u0430 \u043D\u0430 \u043E\u0441\u043D\u043E\u0432\u0435 \u043F\u0440\u043E\u0441\u0442\u043E\u0439 \u043D\u0435\u0438\u0441\u043A\u043B\u044E\u0447\u0438\u0442\u0435\u043B\u044C\u043D\u043E\u0439 \u043B\u0438\u0446\u0435\u043D\u0437\u0438\u0438 \u043D\u0430 \u043F\u0440\u0430\u0432\u043E \u0438\u0441\u043F\u043E\u043B\u044C\u0437\u043E\u0432\u0430\u043D\u0438\u044F \u0431\u0430\u0437\u044B \u0434\u0430\u043D\u043D\u044B\u0445 \u00AB\u0421\u041A\u0420\u0418\u041D\u00BB \u0438 /\u0438\u043B\u0438 \u00AB\u0421\u041A\u0420\u0418\u041D \u041A\u043E\u043D\u0442\u0440\u0430\u0433\u0435\u043D\u0442\u00BB."),
	        React.createElement("ol", null,
	            React.createElement("li", null,
	                React.createElement("h5", null, "\u041E\u0431\u0449\u0438\u0435 \u0443\u0441\u043B\u043E\u0432\u0438\u044F"),
	                React.createElement("ol", null,
	                    React.createElement("li", null, "\u0412 \u043D\u0430\u0441\u0442\u043E\u044F\u0449\u0435\u043C \u041F\u043E\u043B\u044C\u0437\u043E\u0432\u0430\u0442\u0435\u043B\u044C\u0441\u043A\u043E\u043C \u0441\u043E\u0433\u043B\u0430\u0448\u0435\u043D\u0438\u0438 \u0438\u0441\u043F\u043E\u043B\u044C\u0437\u0443\u044E\u0442\u0441\u044F \u0441\u043B\u0435\u0434\u0443\u044E\u0449\u0438\u0435 \u0442\u0435\u0440\u043C\u0438\u043D\u044B \u0438 \u043E\u043F\u0440\u0435\u0434\u0435\u043B\u0435\u043D\u0438\u044F:" + " " + "\u041F\u0440\u0430\u0432\u043E\u043E\u0431\u043B\u0430\u0434\u0430\u0442\u0435\u043B\u044C - \u0410\u043A\u0446\u0438\u043E\u043D\u0435\u0440\u043D\u043E\u0435 \u043E\u0431\u0449\u0435\u0441\u0442\u0432\u043E \u00AB\u0421\u0438\u0441\u0442\u0435\u043C\u0430 \u043A\u043E\u043C\u043F\u043B\u0435\u043A\u0441\u043D\u043E\u0433\u043E \u0440\u0430\u0441\u043A\u0440\u044B\u0442\u0438\u044F \u0438\u043D\u0444\u043E\u0440\u043C\u0430\u0446\u0438\u0438 \u0438 \u043D\u043E\u0432\u043E\u0441\u0442\u0435\u0439\u00BB (\u0410\u041E \u00AB\u0421\u041A\u0420\u0418\u041D\u00BB) (\u041E\u0413\u0420\u041D 1027700088129, \u0420\u043E\u0441\u0441\u0438\u044F, 107023, \u0433. \u041C\u043E\u0441\u043A\u0432\u0430, \u043F\u0435\u0440. \u041C\u0430\u0436\u043E\u0440\u043E\u0432, \u0434\u043E\u043C 14 \u0441\u0442\u0440\u043E\u0435\u043D\u0438\u0435 5, \u043A\u043E\u0442\u043E\u0440\u043E\u043C\u0443 \u043F\u0440\u0438\u043D\u0430\u0434\u043B\u0435\u0436\u0430\u0442 \u0432\u0441\u0435 \u0441\u043E\u043E\u0442\u0432\u0435\u0442\u0441\u0442\u0432\u0443\u044E\u0449\u0438\u0435 \u0438\u043C\u0443\u0449\u0435\u0441\u0442\u0432\u0435\u043D\u043D\u044B\u0435 \u043F\u0440\u0430\u0432\u0430 \u043D\u0430 \u0431\u0430\u0437\u044B \u0434\u0430\u043D\u043D\u044B\u0445 \u00AB\u0421\u041A\u0420\u0418\u041D\u00BB \u0438 \u00AB\u0421\u041A\u0420\u0418\u041D \u041A\u043E\u043D\u0442\u0440\u0430\u0433\u0435\u043D\u0442\u00BB, \u0432\u043A\u043B\u044E\u0447\u0430\u044F \u043F\u0440\u0430\u0432\u0430 \u043D\u0430 \u0434\u043E\u043C\u0435\u043D\u043D\u043E\u0435 \u0438\u043C\u044F http://kontragent.skrin.ru, \u0438 \u043F\u0440\u0435\u0434\u043E\u0441\u0442\u0430\u0432\u043B\u044F\u044E\u0449\u0435\u0435 \u043F\u0440\u0430\u0432\u0430 \u043D\u0430 \u0421\u0435\u0440\u0432\u0438\u0441 \u00AB\u0421\u041A\u0420\u0418\u041D \u041E\u0431\u043B\u0430\u043A\u043E\u00BB \u041F\u043E\u043B\u044C\u0437\u043E\u0432\u0430\u0442\u0435\u043B\u044E." + " " + "\u041F\u043E\u043B\u044C\u0437\u043E\u0432\u0430\u0442\u0435\u043B\u044C \u2013 \u044E\u0440\u0438\u0434\u0438\u0447\u0435\u0441\u043A\u043E\u0435 \u043B\u0438\u0446\u043E / \u0438\u043D\u0434\u0438\u0432\u0438\u0434\u0443\u0430\u043B\u044C\u043D\u044B\u0439 \u043F\u0440\u0435\u0434\u043F\u0440\u0438\u043D\u0438\u043C\u0430\u0442\u0435\u043B\u044C / \u0434\u0435\u0435\u0441\u043F\u043E\u0441\u043E\u0431\u043D\u043E\u0435 \u0444\u0438\u0437\u0438\u0447\u0435\u0441\u043A\u043E\u0435 \u043B\u0438\u0446\u043E, \u0437\u0430\u043A\u043B\u044E\u0447\u0438\u0432\u0448\u0435\u0435 \u0441 \u041F\u0440\u0430\u0432\u043E\u043E\u0431\u043B\u0430\u0434\u0430\u0442\u0435\u043B\u0435\u043C \u043B\u0438\u0446\u0435\u043D\u0437\u0438\u043E\u043D\u043D\u044B\u0439 \u0434\u043E\u0433\u043E\u0432\u043E\u0440, \u043D\u0430 \u043E\u0441\u043D\u043E\u0432\u0435 \u043F\u0440\u043E\u0441\u0442\u043E\u0439 \u043D\u0435\u0438\u0441\u043A\u043B\u044E\u0447\u0438\u0442\u0435\u043B\u044C\u043D\u043E\u0439 \u043B\u0438\u0446\u0435\u043D\u0437\u0438\u0438 \u043D\u0430 \u043F\u0440\u0430\u0432\u043E \u0438\u0441\u043F\u043E\u043B\u044C\u0437\u043E\u0432\u0430\u043D\u0438\u044F \u0431\u0430\u0437 \u0434\u0430\u043D\u043D\u044B\u0445 \u00AB\u0421\u041A\u0420\u0418\u041D\u00BB \u0438 /\u0438\u043B\u0438 \u00AB\u0421\u041A\u0420\u0418\u041D \u041A\u043E\u043D\u0442\u0440\u0430\u0433\u0435\u043D\u0442\u00BB \u0432 \u0440\u0430\u043C\u043A\u0430\u0445 \u043A\u043E\u0442\u043E\u0440\u043E\u0433\u043E, \u0435\u043C\u0443 \u043F\u0440\u0435\u0434\u043E\u0441\u0442\u0430\u0432\u043B\u044F\u0435\u0442\u0441\u044F \u0421\u0435\u0440\u0432\u0438\u0441." + " " + "\u0421\u0435\u0440\u0432\u0438\u0441 - \u043F\u0440\u043E\u0433\u0440\u0430\u043C\u043C\u0430 \u0434\u043B\u044F \u042D\u0412\u041C \u00AB\u0421\u041A\u0420\u0418\u041D \u041E\u0431\u043B\u0430\u043A\u043E\u00BB \u043F\u043E \u0430\u0434\u0440\u0435\u0441\u0443 http://kontragent.skrin.ru.,  \u043F\u0440\u0435\u0434\u043E\u0441\u0442\u0430\u0432\u043B\u044F\u0435\u043C\u0430\u044F \u041F\u0440\u0430\u0432\u043E\u043E\u0431\u043B\u0430\u0434\u0430\u0442\u0435\u043B\u0435\u043C \u041F\u043E\u043B\u044C\u0437\u043E\u0432\u0430\u0442\u0435\u043B\u044E \u0432 \u0440\u0430\u043C\u043A\u0430\u0445 \u0437\u0430\u043A\u043B\u044E\u0447\u0435\u043D\u043D\u043E\u0433\u043E \u043C\u0435\u0436\u0434\u0443 \u043D\u0438\u043C\u0438 \u043B\u0438\u0446\u0435\u043D\u0437\u0438\u043E\u043D\u043D\u043E\u0433\u043E \u0434\u043E\u0433\u043E\u0432\u043E\u0440\u0430 \u043D\u0430 \u043E\u0441\u043D\u043E\u0432\u0435 \u043F\u0440\u043E\u0441\u0442\u043E\u0439 \u043D\u0435\u0438\u0441\u043A\u043B\u044E\u0447\u0438\u0442\u0435\u043B\u044C\u043D\u043E\u0439 \u043B\u0438\u0446\u0435\u043D\u0437\u0438\u0438 \u043D\u0430 \u043F\u0440\u0430\u0432\u043E \u0438\u0441\u043F\u043E\u043B\u044C\u0437\u043E\u0432\u0430\u043D\u0438\u044F \u041F\u043E\u043B\u044C\u0437\u043E\u0432\u0430\u0442\u0435\u043B\u0435\u043C \u0431\u0430\u0437 \u0434\u0430\u043D\u043D\u044B\u0445 \u00AB\u0421\u041A\u0420\u0418\u041D\u00BB \u0438 /\u0438\u043B\u0438 \u00AB\u0421\u041A\u0420\u0418\u041D \u041A\u043E\u043D\u0442\u0440\u0430\u0433\u0435\u043D\u0442\u00BB." + " " + "\u0421\u0430\u0439\u0442 \u2013 \u0418\u043D\u0442\u0435\u0440\u043D\u0435\u0442 \u0441\u0430\u0439\u0442, \u0440\u0430\u0441\u043F\u043E\u043B\u043E\u0436\u0435\u043D\u043D\u044B\u0439 \u043F\u043E \u0430\u0434\u0440\u0435\u0441\u0443: http://kontragent.skrin.ru., \u043D\u0430 \u043A\u043E\u0442\u043E\u0440\u043E\u043C \u041F\u0440\u0430\u0432\u043E\u043E\u0431\u043B\u0430\u0434\u0430\u0442\u0435\u043B\u044C \u043F\u0440\u0435\u0434\u043E\u0441\u0442\u0430\u0432\u043B\u044F\u0435\u0442 \u041F\u043E\u043B\u044C\u0437\u043E\u0432\u0430\u0442\u0435\u043B\u044E, \u0437\u0430\u043A\u043B\u044E\u0447\u0438\u0432\u0448\u0435\u043C\u0443 \u0441 \u041F\u0440\u0430\u0432\u043E\u043E\u0431\u043B\u0430\u0434\u0430\u0442\u0435\u043B\u0435\u043C \u043B\u0438\u0446\u0435\u043D\u0437\u0438\u043E\u043D\u043D\u044B\u0439 \u0434\u043E\u0433\u043E\u0432\u043E\u0440 \u043D\u0430 \u043E\u0441\u043D\u043E\u0432\u0435 \u043F\u0440\u043E\u0441\u0442\u043E\u0439 \u043D\u0435\u0438\u0441\u043A\u043B\u044E\u0447\u0438\u0442\u0435\u043B\u044C\u043D\u043E\u0439 \u043B\u0438\u0446\u0435\u043D\u0437\u0438\u0438 \u043D\u0430 \u043F\u0440\u0430\u0432\u043E \u0438\u0441\u043F\u043E\u043B\u044C\u0437\u043E\u0432\u0430\u043D\u0438\u044F \u041F\u043E\u043B\u044C\u0437\u043E\u0432\u0430\u0442\u0435\u043B\u0435\u043C \u0431\u0430\u0437 \u0434\u0430\u043D\u043D\u044B\u0445 \u00AB\u0421\u041A\u0420\u0418\u041D\u00BB \u0438 /\u0438\u043B\u0438 \u00AB\u0421\u041A\u0420\u0418\u041D \u041A\u043E\u043D\u0442\u0440\u0430\u0433\u0435\u043D\u0442\u00BB \u0434\u043E\u0441\u0442\u0443\u043F \u043A \u0421\u0435\u0440\u0432\u0438\u0441\u0443 \u041F\u0440\u0430\u0432\u043E\u043E\u0431\u043B\u0430\u0434\u0430\u0442\u0435\u043B\u044F."),
	                    React.createElement("li", null, "\u041D\u0430\u0441\u0442\u043E\u044F\u0449\u0435\u0435 \u043F\u043E\u043B\u044C\u0437\u043E\u0432\u0430\u0442\u0435\u043B\u044C\u0441\u043A\u043E\u0435 \u0441\u043E\u0433\u043B\u0430\u0448\u0435\u043D\u0438\u0435 \u044F\u0432\u043B\u044F\u0435\u0442\u0441\u044F \u043F\u0443\u0431\u043B\u0438\u0447\u043D\u043E\u0439 \u043E\u0444\u0435\u0440\u0442\u043E\u0439 \u0432 \u0441\u043E\u043E\u0442\u0432\u0435\u0442\u0441\u0442\u0432\u0438\u0438 \u0441\u043E \u0441\u0442. 437 \u0413\u0440\u0430\u0436\u0434\u0430\u043D\u0441\u043A\u043E\u0433\u043E \u043A\u043E\u0434\u0435\u043A\u0441\u0430 \u0420\u043E\u0441\u0441\u0438\u0439\u0441\u043A\u043E\u0439 \u0424\u0435\u0434\u0435\u0440\u0430\u0446\u0438\u0438 \u0438 \u0432\u0441\u0442\u0443\u043F\u0430\u0435\u0442 \u0432 \u0441\u0438\u043B\u0443 \u0441 \u043C\u043E\u043C\u0435\u043D\u0442\u0430 \u0432\u044B\u0440\u0430\u0436\u0435\u043D\u0438\u044F \u041F\u043E\u043B\u044C\u0437\u043E\u0432\u0430\u0442\u0435\u043B\u0435\u043C \u0441\u043E\u0433\u043B\u0430\u0441\u0438\u044F \u043F\u0443\u0442\u0435\u043C \u043F\u0435\u0440\u0432\u043E\u0433\u043E \u0438\u0441\u043F\u043E\u043B\u044C\u0437\u043E\u0432\u0430\u043D\u0438\u044F \u0421\u0435\u0440\u0432\u0438\u0441\u0430."),
	                    React.createElement("li", null, "\u041A \u043D\u0430\u0441\u0442\u043E\u044F\u0449\u0435\u043C\u0443 \u041F\u043E\u043B\u044C\u0437\u043E\u0432\u0430\u0442\u0435\u043B\u044C\u0441\u043A\u043E\u043C\u0443 \u0441\u043E\u0433\u043B\u0430\u0448\u0435\u043D\u0438\u044E \u0438 \u043E\u0442\u043D\u043E\u0448\u0435\u043D\u0438\u044F\u043C \u043C\u0435\u0436\u0434\u0443 \u041F\u0440\u0430\u0432\u043E\u043E\u0431\u043B\u0430\u0434\u0430\u0442\u0435\u043B\u0435\u043C \u0438 \u041F\u043E\u043B\u044C\u0437\u043E\u0432\u0430\u0442\u0435\u043B\u0435\u043C, \u043A\u043E\u0442\u043E\u0440\u044B\u0435 \u0432\u043E\u0437\u043D\u0438\u043A\u043B\u0438 \u0432 \u0441\u0432\u044F\u0437\u0438 \u0441 \u0438\u0441\u043F\u043E\u043B\u044C\u0437\u043E\u0432\u0430\u043D\u0438\u0435\u043C \u0421\u0435\u0440\u0432\u0438\u0441\u0430, \u043F\u043E\u0434\u043B\u0435\u0436\u0438\u0442 \u043F\u0440\u0438\u043C\u0435\u043D\u0435\u043D\u0438\u044E \u043F\u0440\u0430\u0432\u043E \u0420\u043E\u0441\u0441\u0438\u0439\u0441\u043A\u043E\u0439 \u0424\u0435\u0434\u0435\u0440\u0430\u0446\u0438\u0438."))),
	            React.createElement("li", null,
	                React.createElement("h5", null, "\u041F\u0440\u0435\u0434\u043C\u0435\u0442"),
	                React.createElement("ol", null,
	                    React.createElement("li", null, "\u041F\u0440\u0435\u0434\u043C\u0435\u0442\u043E\u043C \u043D\u0430\u0441\u0442\u043E\u044F\u0449\u0435\u0433\u043E \u041F\u043E\u043B\u044C\u0437\u043E\u0432\u0430\u0442\u0435\u043B\u044C\u0441\u043A\u043E\u0433\u043E \u0441\u043E\u0433\u043B\u0430\u0448\u0435\u043D\u0438\u044F \u044F\u0432\u043B\u044F\u044E\u0442\u0441\u044F \u043F\u0440\u0430\u0432\u0430 \u043D\u0430 \u0438\u0441\u043F\u043E\u043B\u044C\u0437\u043E\u0432\u0430\u043D\u0438\u0435 \u0421\u0435\u0440\u0432\u0438\u0441\u0430 \u041F\u0440\u0430\u0432\u043E\u043E\u0431\u043B\u0430\u0434\u0430\u0442\u0435\u043B\u044F, \u043F\u0440\u0435\u0434\u0441\u0442\u0430\u0432\u043B\u044F\u044E\u0449\u0435\u0433\u043E \u0440\u0430\u0441\u0448\u0438\u0440\u0435\u043D\u043D\u044B\u0435 \u043F\u0440\u0430\u0432\u0430 \u041F\u043E\u043B\u044C\u0437\u043E\u0432\u0430\u0442\u0435\u043B\u044E, \u0437\u0430\u043A\u043B\u044E\u0447\u0438\u0432\u0448\u0435\u043C\u0443 \u0441 \u041F\u0440\u0430\u0432\u043E\u043E\u0431\u043B\u0430\u0434\u0430\u0442\u0435\u043B\u0435\u043C \u043B\u0438\u0446\u0435\u043D\u0437\u0438\u043E\u043D\u043D\u044B\u0439 \u0434\u043E\u0433\u043E\u0432\u043E\u0440 \u043D\u0430 \u043E\u0441\u043D\u043E\u0432\u0435 \u043F\u0440\u043E\u0441\u0442\u043E\u0439 \u043D\u0435\u0438\u0441\u043A\u043B\u044E\u0447\u0438\u0442\u0435\u043B\u044C\u043D\u043E\u0439 \u043B\u0438\u0446\u0435\u043D\u0437\u0438\u0438 \u043D\u0430 \u043F\u0440\u0430\u0432\u043E \u0438\u0441\u043F\u043E\u043B\u044C\u0437\u043E\u0432\u0430\u043D\u0438\u044F \u0431\u0430\u0437 \u0434\u0430\u043D\u043D\u044B\u0445 \u00AB\u0421\u041A\u0420\u0418\u041D\u00BB \u0438 /\u0438\u043B\u0438 \u00AB\u0421\u041A\u0420\u0418\u041D \u041A\u043E\u043D\u0442\u0440\u0430\u0433\u0435\u043D\u0442\u00BB \u043F\u043E \u0430\u0434\u0440\u0435\u0441\u0443  http://kontragent.skrin.ru."),
	                    React.createElement("li", null, "\u041F\u0440\u0435\u0434\u043E\u0441\u0442\u0430\u0432\u043B\u044F\u0435\u043C\u044B\u0439 \u0421\u0435\u0440\u0432\u0438\u0441 \u0441\u043E\u0441\u0442\u043E\u0438\u0442 \u0438\u0437 \u0440\u0430\u0437\u0434\u0435\u043B\u043E\u0432: \u0441\u043F\u0438\u0441\u043E\u043A \u043A\u043E\u043D\u0442\u0430\u043A\u0442\u043E\u0432, \u0437\u0430\u043C\u0435\u0442\u043A\u0438, \u0444\u0430\u0439\u043B\u044B, \u043E\u0442\u0447\u0435\u0442\u044B \u0438 \u043F\u0440\u0435\u0434\u043E\u0441\u0442\u0430\u0432\u043B\u044F\u0435\u0442 \u041F\u043E\u043B\u044C\u0437\u043E\u0432\u0430\u0442\u0435\u043B\u044E \u043F\u0440\u0430\u0432\u043E \u0441\u043E\u0437\u0434\u0430\u0432\u0430\u0442\u044C \u0444\u0430\u0439\u043B\u044B, \u0445\u0440\u0430\u043D\u0438\u0442\u044C \u0441\u043E\u0431\u0441\u0442\u0432\u0435\u043D\u043D\u044B\u0435 \u043A\u043E\u043D\u0442\u0430\u043A\u0442\u044B, \u043E\u0442\u0447\u0435\u0442\u044B, \u043D\u0430\u043F\u043E\u043B\u043D\u044F\u0442\u044C, \u0432\u043D\u043E\u0441\u0438\u0442\u044C \u0438\u0437\u043C\u0435\u043D\u0435\u043D\u0438\u044F, \u0443\u0434\u0430\u043B\u044F\u0442\u044C \u0441\u043E\u0431\u0441\u0442\u0432\u0435\u043D\u043D\u044B\u0435 \u0433\u0440\u0430\u0444\u0438\u0447\u0435\u0441\u043A\u0438\u0435 \u0438 \u0442\u0435\u043A\u0441\u0442\u043E\u0432\u044B\u0435 \u0441\u043E\u0434\u0435\u0440\u0436\u0430\u043D\u0438\u044F \u0432 \u0440\u0430\u0437\u0434\u0435\u043B\u0430\u0445 \u0421\u0435\u0440\u0432\u0438\u0441\u0430 \u043F\u043E \u043A\u0430\u0436\u0434\u043E\u043C\u0443 \u044E\u0440\u0438\u0434\u0438\u0447\u0435\u0441\u043A\u043E\u043C\u0443 \u043B\u0438\u0446\u0443, \u0438\u043D\u0434\u0438\u0432\u0438\u0434\u0443\u0430\u043B\u044C\u043D\u043E\u043C\u0443 \u043F\u0440\u0435\u0434\u043F\u0440\u0438\u043D\u0438\u043C\u0430\u0442\u0435\u043B\u044E, \u0432\u043A\u043B\u044E\u0447\u0435\u043D\u043D\u043E\u043C\u0443 \u0432 \u0431\u0430\u0437\u044B \u0434\u0430\u043D\u043D\u044B\u0445 \u00AB\u0421\u041A\u0420\u0418\u041D\u00BB \u0438 /\u0438\u043B\u0438  \u00AB\u0421\u041A\u0420\u0418\u041D \u041A\u043E\u043D\u0442\u0440\u0430\u0433\u0435\u043D\u0442\u00BB."))),
	            React.createElement("li", null,
	                React.createElement("h5", null, "\u0421\u0440\u043E\u043A \u0434\u0435\u0439\u0441\u0442\u0432\u0438\u044F \u043F\u0440\u0430\u0432\u0430"),
	                React.createElement("ol", null,
	                    React.createElement("li", null, "\u041F\u0440\u0430\u0432\u043E \u043D\u0430 \u0421\u0435\u0440\u0432\u0438\u0441 \u0434\u0435\u0439\u0441\u0442\u0432\u0443\u0435\u0442 \u0432 \u0442\u0435\u0447\u0435\u043D\u0438\u0435 \u0434\u0435\u0439\u0441\u0442\u0432\u0438\u044F \u0437\u0430\u043A\u043B\u044E\u0447\u0435\u043D\u043D\u043E\u0433\u043E \u043B\u0438\u0446\u0435\u043D\u0437\u0438\u043E\u043D\u043D\u043E\u0433\u043E \u0434\u043E\u0433\u043E\u0432\u043E\u0440\u0430 \u043D\u0430 \u043F\u0440\u0430\u0432\u043E \u0438\u0441\u043F\u043E\u043B\u044C\u0437\u043E\u0432\u0430\u043D\u0438\u044F \u0431\u0430\u0437 \u0434\u0430\u043D\u043D\u044B\u0445 \u00AB\u0421\u041A\u0420\u0418\u041D\u00BB \u0438 /\u0438\u043B\u0438  \u00AB\u0421\u041A\u0420\u0418\u041D \u041A\u043E\u043D\u0442\u0440\u0430\u0433\u0435\u043D\u0442\u00BB \u043D\u0430  \u043E\u0441\u043D\u043E\u0432\u0435 \u043F\u0440\u043E\u0441\u0442\u043E\u0439 \u043D\u0435\u0438\u0441\u043A\u043B\u044E\u0447\u0438\u0442\u0435\u043B\u044C\u043D\u043E\u0439 \u043B\u0438\u0446\u0435\u043D\u0437\u0438\u0438."),
	                    React.createElement("li", null,
	                        "\u041F\u0440\u0430\u0432\u043E \u041F\u043E\u043B\u044C\u0437\u043E\u0432\u0430\u0442\u0435\u043B\u044F \u043D\u0430 \u0438\u0441\u043F\u043E\u043B\u044C\u0437\u043E\u0432\u0430\u043D\u0438\u0435 \u0421\u0435\u0440\u0432\u0438\u0441\u0430 \u041F\u0440\u0430\u0432\u043E\u043E\u0431\u043B\u0430\u0434\u0430\u0442\u0435\u043B\u044F \u043F\u0440\u0435\u043A\u0440\u0430\u0449\u0430\u0435\u0442\u0441\u044F \u0441 \u0434\u0430\u0442\u044B: ",
	                        React.createElement("br", null),
	                        "- \u043E\u043A\u043E\u043D\u0447\u0430\u043D\u0438\u044F \u0441\u0440\u043E\u043A\u0430 \u0434\u0435\u0439\u0441\u0442\u0432\u0438\u044F \u043F\u0440\u0430\u0432\u0430 \u041F\u043E\u043B\u044C\u0437\u043E\u0432\u0430\u0442\u0435\u043B\u044F \u043F\u043E \u043B\u0438\u0446\u0435\u043D\u0437\u0438\u043E\u043D\u043D\u043E\u043C\u0443 \u0434\u043E\u0433\u043E\u0432\u043E\u0440\u0443, \u0437\u0430\u043A\u043B\u044E\u0447\u0435\u043D\u043D\u043E\u0433\u043E \u0441 \u041F\u0440\u0430\u0432\u043E\u043E\u0431\u043B\u0430\u0434\u0430\u0442\u0435\u043B\u0435\u043C \u043D\u0430 \u043F\u0440\u0430\u0432\u043E \u0438\u0441\u043F\u043E\u043B\u044C\u0437\u043E\u0432\u0430\u043D\u0438\u044F \u0431\u0430\u0437 \u0434\u0430\u043D\u043D\u044B\u0445 \u00AB\u0421\u041A\u0420\u0418\u041D\u00BB \u0438 /\u0438\u043B\u0438 \u00AB\u0421\u041A\u0420\u0418\u041D \u041A\u043E\u043D\u0442\u0440\u0430\u0433\u0435\u043D\u0442\u00BB;",
	                        React.createElement("br", null),
	                        "- \u0440\u0430\u0441\u0442\u043E\u0440\u0436\u0435\u043D\u0438\u044F \u043B\u0438\u0446\u0435\u043D\u0437\u0438\u043E\u043D\u043D\u043E\u0433\u043E \u0434\u043E\u0433\u043E\u0432\u043E\u0440\u0430, \u0437\u0430\u043A\u043B\u044E\u0447\u0435\u043D\u043D\u043E\u0433\u043E \u043D\u0430 \u043E\u0441\u043D\u043E\u0432\u0435 \u043F\u0440\u043E\u0441\u0442\u043E\u0439 \u043D\u0435\u0438\u0441\u043A\u043B\u044E\u0447\u0438\u0442\u0435\u043B\u044C\u043D\u043E\u0439 \u043B\u0438\u0446\u0435\u043D\u0437\u0438\u0438 \u043D\u0430 \u043F\u0440\u0430\u0432\u043E \u0438\u0441\u043F\u043E\u043B\u044C\u0437\u043E\u0432\u0430\u043D\u0438\u044F  \u041F\u043E\u043B\u044C\u0437\u043E\u0432\u0430\u0442\u0435\u043B\u0435\u043C \u0431\u0430\u0437 \u0434\u0430\u043D\u043D\u044B\u0445 \u00AB\u0421\u041A\u0420\u0418\u041D\u00BB \u0438 /\u0438\u043B\u0438 \u00AB\u0421\u041A\u0420\u0418\u041D \u041A\u043E\u043D\u0442\u0440\u0430\u0433\u0435\u043D\u0442\u00BB \u043F\u043E \u0438\u043D\u0438\u0446\u0438\u0430\u0442\u0438\u0432\u0435 \u041F\u0440\u0430\u0432\u043E\u043E\u0431\u043B\u0430\u0434\u0430\u0442\u0435\u043B\u044F \u0438\u043B\u0438 \u041F\u043E\u043B\u044C\u0437\u043E\u0432\u0430\u0442\u0435\u043B\u044F;",
	                        React.createElement("br", null),
	                        "- \u043D\u0430\u0440\u0443\u0448\u0435\u043D\u0438\u044F \u041F\u043E\u043B\u044C\u0437\u043E\u0432\u0430\u0442\u0435\u043B\u0435\u043C \u043B\u044E\u0431\u043E\u0433\u043E \u0438\u0437 \u043F\u0443\u043D\u043A\u0442\u043E\u0432 \u043D\u0430\u0441\u0442\u043E\u044F\u0449\u0435\u0433\u043E \u041F\u043E\u043B\u044C\u0437\u043E\u0432\u0430\u0442\u0435\u043B\u044C\u0441\u043A\u043E\u0433\u043E \u0441\u043E\u0433\u043B\u0430\u0448\u0435\u043D\u0438\u044F."),
	                    React.createElement("li", null, "\u0412 \u0441\u043B\u0443\u0447\u0430\u0435 \u043D\u0435\u043E\u043F\u043B\u0430\u0442\u044B \u041F\u043E\u043B\u044C\u0437\u043E\u0432\u0430\u0442\u0435\u043B\u0435\u043C \u043F\u0435\u0440\u0438\u043E\u0434\u0430 \u0434\u0435\u0439\u0441\u0442\u0432\u0438\u044F \u043F\u0440\u0430\u0432\u0430 \u043F\u043E \u043B\u0438\u0446\u0435\u043D\u0437\u0438\u043E\u043D\u043D\u043E\u043C\u0443 \u0434\u043E\u0433\u043E\u0432\u043E\u0440\u0443 \u043D\u0430 \u043E\u0441\u043D\u043E\u0432\u0435 \u043F\u0440\u043E\u0441\u0442\u043E\u0439 \u043D\u0435\u0438\u0441\u043A\u043B\u044E\u0447\u0438\u0442\u0435\u043B\u044C\u043D\u043E\u0439 \u043B\u0438\u0446\u0435\u043D\u0437\u0438\u0438 \u043D\u0430 \u043F\u0440\u0430\u0432\u043E \u0438\u0441\u043F\u043E\u043B\u044C\u0437\u043E\u0432\u0430\u043D\u0438\u044F \u041F\u043E\u043B\u044C\u0437\u043E\u0432\u0430\u0442\u0435\u043B\u0435\u043C \u0431\u0430\u0437 \u0434\u0430\u043D\u043D\u044B\u0445 \u00AB\u0421\u041A\u0420\u0418\u041D\u00BB \u0438 /\u0438\u043B\u0438 \u00AB\u0421\u041A\u0420\u0418\u041D \u041A\u043E\u043D\u0442\u0440\u0430\u0433\u0435\u043D\u0442\u00BB, \u0421\u0435\u0440\u0432\u0438\u0441 \u0441\u0442\u0430\u043D\u043E\u0432\u0438\u0442\u0441\u044F \u0434\u043B\u044F \u041F\u043E\u043B\u044C\u0437\u043E\u0432\u0430\u0442\u0435\u043B\u044F \u043D\u0435\u0434\u043E\u0441\u0442\u0443\u043F\u043D\u044B\u043C, \u0431\u0435\u0437 \u0432\u043E\u0437\u043C\u043E\u0436\u043D\u043E\u0441\u0442\u0438 \u043E\u0441\u0443\u0449\u0435\u0441\u0442\u0432\u043B\u0435\u043D\u0438\u044F \u043F\u0440\u0430\u0432, \u043F\u0440\u0435\u0434\u0441\u0442\u0430\u0432\u043B\u0435\u043D\u043D\u044B\u0445 \u041F\u043E\u043B\u044C\u0437\u043E\u0432\u0430\u0442\u0435\u043B\u044E \u0432 \u043F. 2.2. \u043D\u0430\u0441\u0442\u043E\u044F\u0449\u0435\u0433\u043E \u041F\u043E\u043B\u044C\u0437\u043E\u0432\u0430\u0442\u0435\u043B\u044C\u0441\u043A\u043E\u0433\u043E \u0441\u043E\u0433\u043B\u0430\u0448\u0435\u043D\u0438\u044F. \u0421\u0440\u043E\u043A \u043F\u0440\u0438\u043E\u0441\u0442\u0430\u043D\u043E\u0432\u043A\u0438 \u0440\u0430\u0431\u043E\u0442\u044B \u0421\u0435\u0440\u0432\u0438\u0441\u0430 6 (\u0448\u0435\u0441\u0442\u044C) \u043C\u0435\u0441\u044F\u0446\u0435\u0432, \u043F\u043E \u043E\u043A\u043E\u043D\u0447\u0430\u043D\u0438\u0438 \u0443\u043A\u0430\u0437\u0430\u043D\u043D\u043E\u0433\u043E \u0441\u0440\u043E\u043A\u0430, \u041F\u0440\u0430\u0432\u043E\u043E\u0431\u043B\u0430\u0434\u0430\u0442\u0435\u043B\u044C \u0438\u043C\u0435\u0435\u0442 \u043F\u0440\u0430\u0432\u043E \u0443\u0434\u0430\u043B\u0438\u0442\u044C \u0421\u0435\u0440\u0432\u0438\u0441, \u0440\u0430\u043D\u0435\u0435 \u043F\u0440\u0435\u0434\u0441\u0442\u0430\u0432\u043B\u0435\u043D\u043D\u044B\u0439 \u041F\u043E\u043B\u044C\u0437\u043E\u0432\u0430\u0442\u0435\u043B\u044E."),
	                    React.createElement("li", null, "\u0412 \u0441\u043B\u0443\u0447\u0430\u0435 \u043F\u043E\u0441\u0442\u0443\u043F\u043B\u0435\u043D\u0438\u044F \u0432\u043E\u0437\u043D\u0430\u0433\u0440\u0430\u0436\u0434\u0435\u043D\u0438\u044F \u043D\u0430 \u0441\u0447\u0435\u0442 \u041F\u0440\u0430\u0432\u043E\u043E\u0431\u043B\u0430\u0434\u0430\u0442\u0435\u043B\u044F \u0432 \u0440\u0430\u043C\u043A\u0430\u0445 \u043B\u0438\u0446\u0435\u043D\u0437\u0438\u043E\u043D\u043D\u043E\u0433\u043E \u0434\u043E\u0433\u043E\u0432\u043E\u0440\u0430 \u0441 \u041F\u043E\u043B\u044C\u0437\u043E\u0432\u0430\u0442\u0435\u043B\u0435\u043C \u043D\u0430 \u043E\u0441\u043D\u043E\u0432\u0435 \u043F\u0440\u043E\u0441\u0442\u043E\u0439 \u043D\u0435\u0438\u0441\u043A\u043B\u044E\u0447\u0438\u0442\u0435\u043B\u044C\u043D\u043E\u0439 \u043B\u0438\u0446\u0435\u043D\u0437\u0438\u0438 \u043D\u0430 \u043F\u0440\u0430\u0432\u043E \u0438\u0441\u043F\u043E\u043B\u044C\u0437\u043E\u0432\u0430\u043D\u0438\u044F \u0431\u0430\u0437 \u0434\u0430\u043D\u043D\u044B\u0445 \u00AB\u0421\u041A\u0420\u0418\u041D\u00BB \u0438 /\u0438\u043B\u0438 \u00AB\u0421\u041A\u0420\u0418\u041D \u041A\u043E\u043D\u0442\u0440\u0430\u0433\u0435\u043D\u0442\u00BB \u0432 \u0441\u0440\u043E\u043A, \u0443\u043A\u0430\u0437\u0430\u043D\u043D\u044B\u0439 \u0432 \u043F. 3.3. \u043D\u0430\u0441\u0442\u043E\u044F\u0449\u0435\u0433\u043E \u041F\u043E\u043B\u044C\u0437\u043E\u0432\u0430\u0442\u0435\u043B\u044C\u0441\u043A\u043E\u0433\u043E \u0441\u043E\u0433\u043B\u0430\u0448\u0435\u043D\u0438\u044F, \u0421\u0435\u0440\u0432\u0438\u0441 \u043F\u0435\u0440\u0435\u0445\u043E\u0434\u0438\u0442 \u0432 \u043E\u0431\u044B\u0447\u043D\u044B\u0439 \u0440\u0435\u0436\u0438\u043C \u0440\u0430\u0431\u043E\u0442\u044B."))),
	            React.createElement("li", null,
	                React.createElement("h5", null, "\u041F\u0440\u0430\u0432\u0430 \u0438 \u043E\u0431\u044F\u0437\u0430\u043D\u043D\u043E\u0441\u0442\u0438 \u0441\u0442\u043E\u0440\u043E\u043D"),
	                React.createElement("ol", null,
	                    React.createElement("li", null, "\u041F\u0435\u0440\u0435\u0434 \u0442\u0435\u043C \u043A\u0430\u043A \u043D\u0430\u0447\u0430\u0442\u044C \u0438\u0441\u043F\u043E\u043B\u044C\u0437\u043E\u0432\u0430\u0442\u044C \u0421\u0435\u0440\u0432\u0438\u0441, \u041F\u043E\u043B\u044C\u0437\u043E\u0432\u0430\u0442\u0435\u043B\u044C \u043E\u0431\u044F\u0437\u0430\u043D \u043E\u0437\u043D\u0430\u043A\u043E\u043C\u0438\u0442\u044C\u0441\u044F \u0441 \u043D\u0430\u0441\u0442\u043E\u044F\u0449\u0438\u043C \u041F\u043E\u043B\u044C\u0437\u043E\u0432\u0430\u0442\u0435\u043B\u044C\u0441\u043A\u0438\u043C \u0441\u043E\u0433\u043B\u0430\u0448\u0435\u043D\u0438\u0435\u043C."),
	                    React.createElement("li", null, "\u041F\u043E\u043B\u044C\u0437\u043E\u0432\u0430\u0442\u0435\u043B\u044C \u043F\u043E\u043B\u0443\u0447\u0430\u0435\u0442 \u043F\u0440\u0430\u0432\u043E \u0432\u043E\u0441\u043F\u043E\u043B\u044C\u0437\u043E\u0432\u0430\u0442\u044C\u0441\u044F \u0421\u0435\u0440\u0432\u0438\u0441\u043E\u043C, \u0430 \u0438\u043C\u0435\u043D\u043D\u043E \u0441\u0430\u043C\u043E\u0441\u0442\u043E\u044F\u0442\u0435\u043B\u044C\u043D\u043E \u0441\u043E\u0437\u0434\u0430\u0432\u0430\u0442\u044C, \u0438\u0441\u043F\u043E\u043B\u044C\u0437\u043E\u0432\u0430\u0442\u044C \u0438 \u043E\u043F\u0440\u0435\u0434\u0435\u043B\u044F\u0442\u044C \u0435\u0433\u043E \u0441\u043E\u0434\u0435\u0440\u0436\u0430\u043D\u0438\u0435, \u0441 \u0432\u043E\u0437\u043C\u043E\u0436\u043D\u043E\u0441\u0442\u044C\u044E \u0437\u0430\u0433\u0440\u0443\u0437\u043A\u0438, \u0443\u0434\u0430\u043B\u0435\u043D\u0438\u044F \u0444\u0430\u0439\u043B\u043E\u0432, \u0438\u0437\u043C\u0435\u043D\u0435\u043D\u0438\u044F \u0444\u0430\u0439\u043B\u043E\u0432."),
	                    React.createElement("li", null, "\u0412 \u0446\u0435\u043B\u044F\u0445 \u0441\u043E\u0445\u0440\u0430\u043D\u0435\u043D\u0438\u044F \u043A\u043E\u043D\u0444\u0438\u0434\u0435\u043D\u0446\u0438\u0430\u043B\u044C\u043D\u043E\u0441\u0442\u0438 \u0441\u043E\u0434\u0435\u0440\u0436\u0430\u043D\u0438\u044F \u0421\u0435\u0440\u0432\u0438\u0441\u0430, \u041F\u043E\u043B\u044C\u0437\u043E\u0432\u0430\u0442\u0435\u043B\u044C \u043E\u0431\u044F\u0437\u0430\u043D \u043E\u0441\u0443\u0449\u0435\u0441\u0442\u0432\u043B\u044F\u0442\u044C \u0441\u0430\u043C\u043E\u0441\u0442\u043E\u044F\u0442\u0435\u043B\u044C\u043D\u043E\u0435 \u0431\u0435\u0437\u043E\u043F\u0430\u0441\u043D\u043E\u0435 \u0437\u0430\u0432\u0435\u0440\u0448\u0435\u043D\u0438\u0435 \u0440\u0430\u0431\u043E\u0442\u044B \u043F\u043E\u0434 \u0441\u0432\u043E\u0438\u043C \u043F\u0430\u0440\u043E\u043B\u0435\u043C \u043F\u043E \u043E\u043A\u043E\u043D\u0447\u0430\u043D\u0438\u0438 \u043A\u0430\u0436\u0434\u043E\u0439 \u0441\u0435\u0441\u0441\u0438\u0438 \u0440\u0430\u0431\u043E\u0442\u044B \u043D\u0430 \u0441\u0430\u0439\u0442\u0435 http://kontragent.skrin.ru \u0441 \u0438 \u043E\u0431\u0435\u0441\u043F\u0435\u0447\u0438\u0432\u0430\u0442\u044C \u043A\u043E\u043D\u0444\u0438\u0434\u0435\u043D\u0446\u0438\u0430\u043B\u044C\u043D\u043E\u0441\u0442\u044C \u0441\u0432\u043E\u0435\u0433\u043E \u043F\u0430\u0440\u043E\u043B\u044F. \u041F\u043E\u043B\u044C\u0437\u043E\u0432\u0430\u0442\u0435\u043B\u044C \u0433\u0430\u0440\u0430\u043D\u0442\u0438\u0440\u0443\u0435\u0442, \u0447\u0442\u043E \u0438\u043C \u0431\u0443\u0434\u0443\u0442 \u043F\u0440\u0438\u043D\u044F\u0442\u044B \u043D\u0430\u0434\u043B\u0435\u0436\u0430\u0449\u0438\u0435 \u043C\u0435\u0440\u044B \u0434\u043B\u044F \u043E\u0431\u0435\u0441\u043F\u0435\u0447\u0435\u043D\u0438\u044F \u043A\u043E\u043D\u0444\u0438\u0434\u0435\u043D\u0446\u0438\u0430\u043B\u044C\u043D\u043E\u0441\u0442\u0438 \u0443\u0447\u0435\u0442\u043D\u044B\u0445 \u0434\u0430\u043D\u043D\u044B\u0445 (\u043B\u043E\u0433\u0438\u043D\u0430 \u0438 \u043F\u0430\u0440\u043E\u043B\u044F), \u0438\u0441\u043F\u043E\u043B\u044C\u0437\u0443\u0435\u043C\u044B\u0445 \u0438\u043C \u0434\u043B\u044F \u0430\u0432\u0442\u043E\u0440\u0438\u0437\u0430\u0446\u0438\u0438 \u043D\u0430 \u0441\u0430\u0439\u0442\u0435 \u0438 \u043F\u0440\u0435\u0434\u043E\u0442\u0432\u0440\u0430\u0449\u0435\u043D\u0438\u044F \u0432\u043E\u0437\u043C\u043E\u0436\u043D\u043E\u0441\u0442\u0438 \u0430\u0432\u0442\u043E\u0440\u0438\u0437\u0430\u0446\u0438\u0438 \u0442\u0440\u0435\u0442\u044C\u0438\u043C\u0438 \u043B\u0438\u0446\u0430\u043C\u0438."),
	                    React.createElement("li", null, "\u041F\u0440\u0430\u0432\u043E\u043E\u0431\u043B\u0430\u0434\u0430\u0442\u0435\u043B\u044C \u043E\u0431\u044F\u0437\u0443\u0435\u0442\u0441\u044F \u043E\u0431\u0435\u0441\u043F\u0435\u0447\u0438\u0442\u044C \u0442\u0435\u0445\u043D\u0438\u0447\u0435\u0441\u043A\u0443\u044E \u0432\u043E\u0437\u043C\u043E\u0436\u043D\u043E\u0441\u0442\u044C \u0434\u043E\u0441\u0442\u0443\u043F\u0430 \u041F\u043E\u043B\u044C\u0437\u043E\u0432\u0430\u0442\u0435\u043B\u044F \u043A \u0421\u0435\u0440\u0432\u0438\u0441\u0443 \u0441 \u0438\u0441\u043F\u043E\u043B\u044C\u0437\u043E\u0432\u0430\u043D\u0438\u0435\u043C \u0438\u043D\u0434\u0438\u0432\u0438\u0434\u0443\u0430\u043B\u044C\u043D\u043E\u0433\u043E \u0441\u0438\u0441\u0442\u0435\u043C\u043D\u043E\u0433\u043E \u0438\u043C\u0435\u043D\u0438 \u0438 \u043F\u0430\u0440\u043E\u043B\u044F \u0442\u043E\u043B\u044C\u043A\u043E \u043F\u0440\u0438 \u0443\u0441\u043B\u043E\u0432\u0438\u0438, \u0447\u0442\u043E \u0442\u0435\u0445\u043D\u0438\u0447\u0435\u0441\u043A\u043E\u0435 \u043E\u0441\u043D\u0430\u0449\u0435\u043D\u0438\u0435 \u041F\u043E\u043B\u044C\u0437\u043E\u0432\u0430\u0442\u0435\u043B\u044F \u043E\u0442\u0432\u0435\u0447\u0430\u0435\u0442 \u0441\u043B\u0435\u0434\u0443\u044E\u0449\u0438\u043C \u0442\u0440\u0435\u0431\u043E\u0432\u0430\u043D\u0438\u044F\u043C: \u0441\u0440\u0435\u0434\u0441\u0442\u0432\u0430 \u0434\u043E\u0441\u0442\u0443\u043F\u0430 \u0432 \u0433\u043B\u043E\u0431\u0430\u043B\u044C\u043D\u0443\u044E \u0441\u0435\u0442\u044C \u0418\u043D\u0442\u0435\u0440\u043D\u0435\u0442 \u0441\u043E \u0441\u043A\u043E\u0440\u043E\u0441\u0442\u044C\u044E \u043D\u0435 \u043C\u0435\u043D\u0435\u0435 32000 bps; \u043A\u043E\u043C\u043F\u044C\u044E\u0442\u0435\u0440 \u0441 \u0443\u0441\u0442\u0430\u043D\u043E\u0432\u043B\u0435\u043D\u043D\u043E\u0439 \u043E\u043F\u0435\u0440\u0430\u0446\u0438\u043E\u043D\u043D\u043E\u0439 \u0441\u0438\u0441\u0442\u0435\u043C\u043E\u0439 Windows 7,8, 10, Microsoft Internet Explorer 10 \u0438\u043B\u0438 \u0441\u0442\u0430\u0440\u0448\u0435, Firefox \u0438 Chrome \u0434\u0432\u0443\u0445 \u043F\u043E\u0441\u043B\u0435\u0434\u043D\u0438\u0445 \u0432\u0435\u0440\u0441\u0438\u0439. \u0418\u0441\u043F\u043E\u043B\u044C\u0437\u043E\u0432\u0430\u043D\u0438\u0435 \u0434\u0440\u0443\u0433\u0438\u0445 \u0442\u0438\u043F\u043E\u0432 \u043E\u043F\u0435\u0440\u0430\u0446\u0438\u043E\u043D\u043D\u044B\u0445 \u0441\u0438\u0441\u0442\u0435\u043C \u0438/\u0438\u043B\u0438 \u0434\u0440\u0443\u0433\u0438\u0445 \u0431\u0440\u0430\u0443\u0437\u0435\u0440\u043E\u0432 \u0432\u043E\u0437\u043C\u043E\u0436\u043D\u043E, \u043D\u043E \u043F\u0440\u0438 \u0438\u0445 \u0438\u0441\u043F\u043E\u043B\u044C\u0437\u043E\u0432\u0430\u043D\u0438\u0438 \u043D\u0435 \u0433\u0430\u0440\u0430\u043D\u0442\u0438\u0440\u0443\u0435\u0442\u0441\u044F \u0434\u043E\u0441\u0442\u0443\u043F \u043A\u043E \u0432\u0441\u0435\u043C \u0444\u0443\u043D\u043A\u0446\u0438\u044F\u043C \u0421\u0435\u0440\u0432\u0438\u0441\u0430."),
	                    React.createElement("li", null, "\u041F\u043E\u043B\u044C\u0437\u043E\u0432\u0430\u0442\u0435\u043B\u044C \u043E\u0431\u044F\u0437\u0430\u043D \u0441\u043E\u0431\u043B\u044E\u0434\u0430\u0442\u044C \u0443\u0441\u043B\u043E\u0432\u0438\u044F \u043D\u0430\u0441\u0442\u043E\u044F\u0449\u0435\u0433\u043E \u041F\u043E\u043B\u044C\u0437\u043E\u0432\u0430\u0442\u0435\u043B\u044C\u0441\u043A\u043E\u0433\u043E \u0421\u043E\u0433\u043B\u0430\u0448\u0435\u043D\u0438\u044F \u0431\u0435\u0437 \u043A\u0430\u043A\u0438\u0445 \u043B\u0438\u0431\u043E \u043E\u0433\u0440\u0430\u043D\u0438\u0447\u0435\u043D\u0438\u0439."),
	                    React.createElement("li", null, "\u041F\u0440\u0430\u0432\u043E\u043E\u0431\u043B\u0430\u0434\u0430\u0442\u0435\u043B\u044C \u0432\u043F\u0440\u0430\u0432\u0435 \u0432 \u043B\u044E\u0431\u043E\u0435 \u0432\u0440\u0435\u043C\u044F \u0432 \u043E\u0434\u043D\u043E\u0441\u0442\u043E\u0440\u043E\u043D\u043D\u0435\u043C \u043F\u043E\u0440\u044F\u0434\u043A\u0435 \u043E\u0431\u043D\u043E\u0432\u043B\u044F\u0442\u044C \u0432\u0435\u0440\u0441\u0438\u044E \u0421\u0435\u0440\u0432\u0438\u0441\u0430."))),
	            React.createElement("li", null,
	                React.createElement("h5", null, "\u041E\u0442\u0432\u0435\u0442\u0441\u0442\u0432\u0435\u043D\u043D\u043E\u0441\u0442\u044C"),
	                React.createElement("ol", null,
	                    React.createElement("li", null, "\u0417\u0430 \u043D\u0435\u0438\u0441\u043F\u043E\u043B\u043D\u0435\u043D\u0438\u0435 \u0438\u043B\u0438 \u043D\u0435\u043D\u0430\u0434\u043B\u0435\u0436\u0430\u0449\u0435\u0435 \u0438\u0441\u043F\u043E\u043B\u043D\u0435\u043D\u0438\u0435 \u0443\u0441\u043B\u043E\u0432\u0438\u0439 \u043D\u0430\u0441\u0442\u043E\u044F\u0449\u0435\u0433\u043E \u041F\u043E\u043B\u044C\u0437\u043E\u0432\u0430\u0442\u0435\u043B\u044C\u0441\u043A\u043E\u0433\u043E \u0421\u043E\u0433\u043B\u0430\u0448\u0435\u043D\u0438\u044F \u041F\u0440\u0430\u0432\u043E\u043E\u0431\u043B\u0430\u0434\u0430\u0442\u0435\u043B\u044C \u0438 \u041F\u043E\u043B\u044C\u0437\u043E\u0432\u0430\u0442\u0435\u043B\u044C \u043D\u0435\u0441\u0443\u0442 \u043E\u0442\u0432\u0435\u0442\u0441\u0442\u0432\u0435\u043D\u043D\u043E\u0441\u0442\u044C \u0432 \u0441\u043E\u043E\u0442\u0432\u0435\u0442\u0441\u0442\u0432\u0438\u0438 \u0441 \u0437\u0430\u043A\u043E\u043D\u043E\u0434\u0430\u0442\u0435\u043B\u044C\u0441\u0442\u0432\u043E\u043C \u0420\u0424."),
	                    React.createElement("li", null, "\u041F\u043E\u043B\u044C\u0437\u043E\u0432\u0430\u0442\u0435\u043B\u044C \u043D\u0435\u0441\u0435\u0442 \u043F\u043E\u043B\u043D\u0443\u044E \u043E\u0442\u0432\u0435\u0442\u0441\u0442\u0432\u0435\u043D\u043D\u043E\u0441\u0442\u044C \u0437\u0430 \u0441\u0432\u043E\u0438 \u0434\u0435\u0439\u0441\u0442\u0432\u0438\u044F, \u0441\u0432\u044F\u0437\u0430\u043D\u043D\u044B\u0435 \u0441 \u0444\u043E\u0440\u043C\u0438\u0440\u043E\u0432\u0430\u043D\u0438\u0435\u043C \u0438 \u0438\u0441\u043F\u043E\u043B\u044C\u0437\u043E\u0432\u0430\u043D\u0438\u0435\u043C \u0441\u043E\u0434\u0435\u0440\u0436\u0430\u043D\u0438\u044F \u0421\u0435\u0440\u0432\u0438\u0441\u0430, \u043A\u043E\u043D\u0442\u0440\u043E\u043B\u0435\u043C \u0434\u043E\u0441\u0442\u0443\u043F\u0430 \u0442\u0440\u0435\u0442\u044C\u0438\u0445 \u043B\u0438\u0446 \u043A \u0421\u0435\u0440\u0432\u0438\u0441\u0443, \u0432 \u0441\u043E\u043E\u0442\u0432\u0435\u0442\u0441\u0442\u0432\u0438\u0438 \u0441 \u0434\u0435\u0439\u0441\u0442\u0432\u0443\u044E\u0449\u0438\u043C \u0437\u0430\u043A\u043E\u043D\u043E\u0434\u0430\u0442\u0435\u043B\u044C\u0441\u0442\u0432\u043E\u043C \u0420\u043E\u0441\u0441\u0438\u0439\u0441\u043A\u043E\u0439 \u0424\u0435\u0434\u0435\u0440\u0430\u0446\u0438\u0438."),
	                    React.createElement("li", null, "\u041F\u0440\u0430\u0432\u043E\u043E\u0431\u043B\u0430\u0434\u0430\u0442\u0435\u043B\u044C \u043D\u0435 \u043D\u0435\u0441\u0435\u0442 \u043E\u0442\u0432\u0435\u0442\u0441\u0442\u0432\u0435\u043D\u043D\u043E\u0441\u0442\u0438 \u0437\u0430 \u0432\u043E\u0437\u043C\u043E\u0436\u043D\u044B\u0435 \u0441\u0431\u043E\u0438 \u0438 \u043F\u0435\u0440\u0435\u0440\u044B\u0432\u044B \u0432 \u0440\u0430\u0431\u043E\u0442\u0435  \u0421\u0435\u0440\u0432\u0438\u0441\u0430 \u0438 \u0432\u044B\u0437\u0432\u0430\u043D\u043D\u0443\u044E \u0438\u043C\u0438 \u043F\u043E\u0442\u0435\u0440\u044E \u0438\u043D\u0444\u043E\u0440\u043C\u0430\u0446\u0438\u0438."),
	                    React.createElement("li", null, "\u041F\u0440\u0430\u0432\u043E\u043E\u0431\u043B\u0430\u0434\u0430\u0442\u0435\u043B\u044C \u043D\u0435 \u043D\u0435\u0441\u0435\u0442 \u043E\u0442\u0432\u0435\u0442\u0441\u0442\u0432\u0435\u043D\u043D\u043E\u0441\u0442\u0438 \u0437\u0430 \u043B\u044E\u0431\u043E\u0439 \u0443\u0449\u0435\u0440\u0431 \u043A\u043E\u043C\u043F\u044C\u044E\u0442\u0435\u0440\u0443 \u041F\u043E\u043B\u044C\u0437\u043E\u0432\u0430\u0442\u0435\u043B\u044F,  \u043C\u043E\u0431\u0438\u043B\u044C\u043D\u044B\u043C \u0443\u0441\u0442\u0440\u043E\u0439\u0441\u0442\u0432\u0430\u043C, \u043B\u044E\u0431\u043E\u043C\u0443 \u0434\u0440\u0443\u0433\u043E\u043C\u0443 \u043E\u0431\u043E\u0440\u0443\u0434\u043E\u0432\u0430\u043D\u0438\u044E \u0438\u043B\u0438 \u043F\u0440\u043E\u0433\u0440\u0430\u043C\u043C\u043D\u043E\u043C\u0443 \u043E\u0431\u0435\u0441\u043F\u0435\u0447\u0435\u043D\u0438\u044E, \u0432\u044B\u0437\u0432\u0430\u043D\u043D\u044B\u0439 \u0438\u043B\u0438 \u0441\u0432\u044F\u0437\u0430\u043D\u043D\u044B\u0439 \u0441 \u0438\u0441\u043F\u043E\u043B\u044C\u0437\u043E\u0432\u0430\u043D\u0438\u0435\u043C \u0421\u0435\u0440\u0432\u0438\u0441\u0430, \u043F\u0440\u043E\u0438\u0437\u043E\u0448\u0435\u0434\u0448\u0438\u0439 \u043F\u043E \u043D\u0435 \u0437\u0430\u0432\u0438\u0441\u044F\u0449\u0438\u043C \u043E\u0442 \u041F\u0440\u0430\u0432\u043E\u043E\u0431\u043B\u0430\u0434\u0430\u0442\u0435\u043B\u044F \u043F\u0440\u0438\u0447\u0438\u043D\u0430\u043C \u0438 / \u0438\u043B\u0438 \u0437\u0430 \u043B\u044E\u0431\u043E\u0439 \u0443\u0449\u0435\u0440\u0431, \u0432\u043A\u043B\u044E\u0447\u0430\u044F \u0443\u043F\u0443\u0449\u0435\u043D\u043D\u0443\u044E \u0432\u044B\u0433\u043E\u0434\u0443, \u0438\u043B\u0438 \u0432\u0440\u0435\u0434, \u0432\u044B\u0437\u0432\u0430\u043D\u043D\u044B\u0435 \u0432 \u0441\u0432\u044F\u0437\u0438 \u0441 \u0438\u0441\u043F\u043E\u043B\u044C\u0437\u043E\u0432\u0430\u043D\u0438\u0435\u043C \u0421\u0435\u0440\u0432\u0438\u0441\u0430."),
	                    React.createElement("li", null, "\u041F\u0440\u0430\u0432\u043E\u043E\u0431\u043B\u0430\u0434\u0430\u0442\u0435\u043B\u044C, \u043F\u0440\u0435\u0434\u043E\u0441\u0442\u0430\u0432\u043B\u044F\u044F \u041F\u043E\u043B\u044C\u0437\u043E\u0432\u0430\u0442\u0435\u043B\u044E \u0442\u0435\u0445\u043D\u0438\u0447\u0435\u0441\u043A\u0443\u044E \u0432\u043E\u0437\u043C\u043E\u0436\u043D\u043E\u0441\u0442\u044C \u0438\u0441\u043F\u043E\u043B\u044C\u0437\u043E\u0432\u0430\u043D\u0438\u044F \u0421\u0435\u0440\u0432\u0438\u0441\u0430 \u043D\u0430 \u0441\u0430\u0439\u0442\u0435 http://kontragent.skrin.ru \u043D\u0435 \u0438\u043C\u0435\u0435\u0442 \u0434\u043E\u0441\u0442\u0443\u043F \u043A \u043D\u0430\u043F\u043E\u043B\u043D\u0435\u043D\u0438\u044E \u0421\u0435\u0440\u0432\u0438\u0441\u0430 \u041F\u043E\u043B\u044C\u0437\u043E\u0432\u0430\u0442\u0435\u043B\u0435\u043C, \u043D\u0435 \u0438\u043C\u0435\u0435\u0442 \u0432\u043E\u0437\u043C\u043E\u0436\u043D\u043E\u0441\u0442\u0438 \u043A\u043E\u043D\u0442\u0440\u043E\u043B\u0438\u0440\u043E\u0432\u0430\u0442\u044C \u0434\u0435\u0439\u0441\u0442\u0432\u0438\u044F \u041F\u043E\u043B\u044C\u0437\u043E\u0432\u0430\u0442\u0435\u043B\u044F, \u043E\u0441\u0443\u0449\u0435\u0441\u0442\u0432\u043B\u044F\u0442\u044C \u0430\u0432\u0442\u043E\u043C\u0430\u0442\u0438\u0447\u0435\u0441\u043A\u0443\u044E \u0446\u0435\u043D\u0437\u0443\u0440\u0443 \u0438\u043B\u0438 \u043C\u043E\u0434\u0435\u0440\u0430\u0446\u0438\u044E \u0441\u043E\u0434\u0435\u0440\u0436\u0430\u043D\u0438\u044F \u0421\u0435\u0440\u0432\u0438\u0441\u0430 \u0438 \u043D\u0435 \u043D\u0435\u0441\u0435\u0442 \u043E\u0442\u0432\u0435\u0442\u0441\u0442\u0432\u0435\u043D\u043D\u043E\u0441\u0442\u0438 \u0437\u0430 \u0434\u0435\u0439\u0441\u0442\u0432\u0438\u044F \u0438\u043B\u0438 \u0431\u0435\u0437\u0434\u0435\u0439\u0441\u0442\u0432\u0438\u0435 \u041F\u043E\u043B\u044C\u0437\u043E\u0432\u0430\u0442\u0435\u043B\u044F \u0438 /\u0438\u043B\u0438 \u0442\u0440\u0435\u0442\u044C\u0438\u0445 \u043B\u0438\u0446."),
	                    React.createElement("li", null, "\u041F\u0440\u0430\u0432\u043E\u043E\u0431\u043B\u0430\u0434\u0430\u0442\u0435\u043B\u044C \u043D\u0435 \u043D\u0435\u0441\u0435\u0442 \u043E\u0442\u0432\u0435\u0442\u0441\u0442\u0432\u0435\u043D\u043D\u043E\u0441\u0442\u044C \u043F\u0435\u0440\u0435\u0434 \u041F\u043E\u043B\u044C\u0437\u043E\u0432\u0430\u0442\u0435\u043B\u0435\u043C \u0438\u043B\u0438 \u043B\u044E\u0431\u044B\u043C\u0438 \u0442\u0440\u0435\u0442\u044C\u0438\u043C\u0438 \u043B\u0438\u0446\u0430\u043C\u0438 \u0437\u0430 \u043B\u044E\u0431\u043E\u0439 \u0443\u0449\u0435\u0440\u0431, \u0432\u043A\u043B\u044E\u0447\u0430\u044F \u0443\u043F\u0443\u0449\u0435\u043D\u043D\u0443\u044E \u0432\u044B\u0433\u043E\u0434\u0443, \u0432\u0440\u0435\u0434 \u0447\u0435\u0441\u0442\u0438, \u0434\u043E\u0441\u0442\u043E\u0438\u043D\u0441\u0442\u0432\u0443 \u0438\u043B\u0438 \u0434\u0435\u043B\u043E\u0432\u043E\u0439 \u0440\u0435\u043F\u0443\u0442\u0430\u0446\u0438\u0438, \u043D\u0430\u0441\u0442\u0443\u043F\u0438\u0432\u0448\u0438\u0435 \u043A\u0430\u043A \u0441\u043B\u0435\u0434\u0441\u0442\u0432\u0438\u0435 \u0438\u0441\u043F\u043E\u043B\u044C\u0437\u043E\u0432\u0430\u043D\u0438\u044F \u0421\u0435\u0440\u0432\u0438\u0441\u0430 \u041F\u043E\u043B\u044C\u0437\u043E\u0432\u0430\u0442\u0435\u043B\u0435\u043C."))),
	            React.createElement("li", null,
	                React.createElement("h5", null, "\u0412\u0441\u0442\u0443\u043F\u043B\u0435\u043D\u0438\u0435 \u0432 \u0441\u0438\u043B\u0443"),
	                React.createElement("ol", null,
	                    React.createElement("li", null, "\u041D\u0430\u0441\u0442\u043E\u044F\u0449\u0435\u0435 \u041F\u043E\u043B\u044C\u0437\u043E\u0432\u0430\u0442\u0435\u043B\u044C\u0441\u043A\u043E\u0435  \u0441\u043E\u0433\u043B\u0430\u0448\u0435\u043D\u0438\u0435 \u0432\u0441\u0442\u0443\u043F\u0430\u0435\u0442 \u0432 \u0441\u0438\u043B\u0443 \u0441 \u043C\u043E\u043C\u0435\u043D\u0442\u0430 \u0440\u0430\u0437\u043C\u0435\u0449\u0435\u043D\u0438\u044F \u0435\u0433\u043E \u0432 \u0441\u0435\u0442\u0438 \u0438\u043D\u0442\u0435\u0440\u043D\u0435\u0442 \u043F\u043E \u0430\u0434\u0440\u0435\u0441\u0443: http://kontragent.skrin.ru."),
	                    React.createElement("li", null, "\u041D\u0430\u0441\u0442\u043E\u044F\u0449\u0435\u0435 \u041F\u043E\u043B\u044C\u0437\u043E\u0432\u0430\u0442\u0435\u043B\u044C\u0441\u043A\u043E\u0435 \u0441\u043E\u0433\u043B\u0430\u0448\u0435\u043D\u0438\u0435 \u043C\u043E\u0436\u0435\u0442 \u0431\u044B\u0442\u044C \u0438\u0437\u043C\u0435\u043D\u0435\u043D\u043E \u041F\u0440\u0430\u0432\u043E\u043E\u0431\u043B\u0430\u0434\u0430\u0442\u0435\u043B\u0435\u043C \u0432 \u043E\u0434\u043D\u043E\u0441\u0442\u043E\u0440\u043E\u043D\u043D\u0435\u043C \u043F\u043E\u0440\u044F\u0434\u043A\u0435, \u0431\u0435\u0437 \u0443\u0432\u0435\u0434\u043E\u043C\u043B\u0435\u043D\u0438\u044F \u041F\u043E\u043B\u044C\u0437\u043E\u0432\u0430\u0442\u0435\u043B\u044F, \u043D\u043E\u0432\u0430\u044F \u0440\u0435\u0434\u0430\u043A\u0446\u0438\u044F \u041F\u043E\u043B\u044C\u0437\u043E\u0432\u0430\u0442\u0435\u043B\u044C\u0441\u043A\u043E\u0433\u043E \u0421\u043E\u0433\u043B\u0430\u0448\u0435\u043D\u0438\u044F \u0432\u0441\u0442\u0443\u043F\u0430\u0435\u0442 \u0432 \u0441\u0438\u043B\u0443 \u0441 \u043C\u043E\u043C\u0435\u043D\u0442\u0430 \u0435\u0435 \u0440\u0430\u0437\u043C\u0435\u0449\u0435\u043D\u0438\u044F \u0435\u0433\u043E \u0432 \u0441\u0435\u0442\u0438 \u0438\u043D\u0442\u0435\u0440\u043D\u0435\u0442 \u043F\u043E \u0430\u0434\u0440\u0435\u0441\u0443:  http://kontragent.skrin.ru."))))));
	}
	exports.getTerms = getTerms;


/***/ }
/******/ ]);
//# sourceMappingURL=iss.bundle.js.map