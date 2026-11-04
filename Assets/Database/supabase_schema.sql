-- Starter schema only. Review before applying to a Supabase project.
create table if not exists public.player_profiles (
  user_id uuid primary key references auth.users(id) on delete cascade,
  display_name text not null default 'Player',
  level integer not null default 1 check (level >= 1),
  xp bigint not null default 0 check (xp >= 0),
  created_at timestamptz not null default now(),
  updated_at timestamptz not null default now()
);

create table if not exists public.player_stats (
  user_id uuid primary key references auth.users(id) on delete cascade,
  matches_played integer not null default 0,
  wins integer not null default 0,
  losses integer not null default 0,
  eliminations integer not null default 0,
  deaths integer not null default 0,
  updated_at timestamptz not null default now()
);

alter table public.player_profiles enable row level security;
alter table public.player_stats enable row level security;

create policy "Profiles readable by authenticated users"
on public.player_profiles for select to authenticated using (true);

create policy "Users insert own profile"
on public.player_profiles for insert to authenticated with check (auth.uid() = user_id);

create policy "Users update own profile"
on public.player_profiles for update to authenticated
using (auth.uid() = user_id) with check (auth.uid() = user_id);

create policy "Stats readable by authenticated users"
on public.player_stats for select to authenticated using (true);

-- Stats writes should be performed by trusted server functions, not directly by clients.
