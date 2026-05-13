## v0.3.0

#### EndPoints
- `EndPoints.Rooms` の URL `/Rooms` (大文字) を `/rooms` に修正
- `EndPoints.RoomFilesOf` の URL に混入していた連続空白 (`v    2`) を `v2` に修正
- `EndPoints.RoomTasksOfStatus(roomId, taskId)` を新規追加 (PUT `/rooms/{id}/tasks/{tid}/status` 用)

#### RoomQuery
- `CreateAsync`: GET → POST に修正
- [破壊的] `CreateAsync` の signature を spec 準拠に拡張: `()` → `(name, adminMemberIds, description?, normalMemberIds?, readonlyMemberIds?, iconPreset?)`。必須の `name` / `members_admin_ids` を form body に含めるようにし、optional 引数は null の場合 form から省略
- `UpdateAsync`: POST → PUT に修正、`description` form 値に `roomName` を入れていたバグを修正
- `DeleteAsync` / `LeaveAsync`: 誤った POST `/rooms/{id}/messages?action_type=...` → 正しい DELETE `/rooms/{id}` + `action_type` form body に修正

#### RoomMessageQuery
- `ReadAsync`: POST `/rooms/{id}/messages?message_id=...` → PUT `/rooms/{id}/messages/read` + `message_id` form body
- `UnReadAsync`: POST `/rooms/{id}/messages?message_id=...` → PUT `/rooms/{id}/messages/unread` + `message_id` form body
- `UpdateAsync`: POST `/rooms/{id}/messages/{mid}?body=...` → PUT `/rooms/{id}/messages/{mid}` + `body` form body

#### RoomTaskQuery
- `GetAllAsync`: フィルタ (`account_id` / `assigned_by_account_id` / `status`) を form body → query string へ移動
- [破壊的] `GetAllAsync` の signature を `(roomId, accountId, autherId, bool isDone)` → `(roomId, accountId?, assignedByAccountId?, TaskStateType? status)` に変更。すべての filter を nullable 化し、null の場合は query string から省略
- `UpdateAsync`: POST `/rooms/{id}/tasks/{tid}?body=...` → PUT `/rooms/{id}/tasks/{tid}/status` + `body` form body
- [破壊的] `CreateAsync`: `NotImplementedException` → POST `/rooms/{id}/tasks` を実装。signature を `(roomId, taskText, limit)` → `(roomId, taskText, toIds, limit, limitType)` に変更 (spec の必須 `to_ids` / `limit_type` 追加)。戻り値も `TaskId` → `TaskIds` (新 DTO、`long[] task_ids`) に変更
- 新規 `TaskLimitType` enum 追加 (`none` / `date` / `time`)
- 新規 `TaskIds` DTO 追加 (`long[] task_ids`)

#### RoomFileQuery
- `GetAsync`: URL に `file_id` が含まれていなかったバグを修正 (`EndPoints.RoomFiles(roomId)` → `EndPoints.RoomFilesOf(roomId, fileId)`)
- [破壊的] `GetAllAsync`: `accountId: long` → `accountId: long? = null` に変更。null の場合 `?account_id=` query を省略

#### RoomInviteQuery
- `GetAsync`: POST → GET に修正
- `CreateAsync`: 誤って `EndPoints.RoomTasks(id)` を叩いていた → `EndPoints.RoomLink(id)` に修正
- `UpdateAsync`: 同上、`EndPoints.RoomTasks(id)` → `EndPoints.RoomLink(id)`

#### IncomingRequestQuery
- `AcceptAsync`: POST → PUT に修正

#### RoomMemberQuery
- `UpdateAsync`: `NotImplementedException` → PUT `/rooms/{id}/members` + `members_admin_ids` / `members_member_ids` / `members_readonly_ids` (long のカンマ区切り) form body を実装

#### OAuth2Client
- `Authorization` メソッドに `openBrowser: Action<string>` と `readCode: Func<string>` の任意引数を追加。null の場合は従来通り `Process.Start` / `Console.ReadLine` を使うのでバイナリ互換維持。テスト時はコールバックを差し替えて副作用なしに検証可能
