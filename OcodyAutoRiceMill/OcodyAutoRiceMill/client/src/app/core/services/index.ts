import { StorageService } from "./storage.service";
import {
  AuthTokenFactory,
  AuthTokenService
} from "./auth-token.service";
import { AuthService } from "./auth.service";
import { TokenInterceptor } from "./token.interceptor";
import { JsonApiService } from "./json-api.service";
import { UserService } from "./user.service";
import { ChatService } from "./chat.service";
import { NotificationService } from "./notification.service";
import { BodyService } from "./body.service";
import { LayoutService } from "./layout.service";
import { SoundService } from "./sound.service";


import * as fromVoice from './voice'

export const services = [
  StorageService,
  AuthTokenService,
  AuthService,
  TokenInterceptor,

  JsonApiService,
  UserService,
  ChatService,
  NotificationService,
  BodyService,
  LayoutService,
  SoundService,
  fromVoice.VoiceControlService,
  fromVoice.VoiceRecognitionService,
];

export * from "./storage.service";
export * from "./auth-token.service";
export * from "./auth.service";
export * from "./token.interceptor";
export * from "./json-api.service";
export * from "./user.service";
export * from "./chat.service";
export * from "./notification.service";
export * from "./body.service";
export * from "./layout.service";
export * from "./sound.service";
export * from "./voice";
